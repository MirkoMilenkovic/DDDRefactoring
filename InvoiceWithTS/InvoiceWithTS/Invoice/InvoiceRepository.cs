using InvoiceWithTS.Common;
using InvoiceWithTS.DB;
using InvoiceWithTS.Invoice.BusinessModel;
using InvoiceWithTS.Invoice.DTO;
using InvoiceWithTS.Invoice.UseCases.GetAllInvoices;
using System.Transactions;

namespace InvoiceWithTS.Invoice
{
    public class InvoiceRepository
    {
        private InMemoryDB _db;

        private ILogger<InvoiceRepository> _logger;

        public InvoiceRepository(
            InMemoryDB db, 
            ILogger<InvoiceRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public InvoiceModel? GetById(int id)
        {
            // SELECT by Id
            _db.Invoice.TryGetValue(id, out var invoiceDto);

            if (invoiceDto == null)
            {
                // not found
                return null;
            }

            // SELECT by InvoiceId
            IEnumerable<DTO.InvoiceItemDTO> invoiceItemDtoList = _db.InvoiceItem.Values
                .Where(x => x.InvoiceId == id);

            InvoiceModel invoiceModel = InvoiceModel.FromDTO(
                invoiceDto: invoiceDto,
                itemDtoList: invoiceItemDtoList,
                entityState: Common.EntityStates.Loaded);

            return invoiceModel;
        }

        public void Save(InvoiceModel invoiceModel)
        {
            using TransactionScope ts = new TransactionScope(TransactionScopeOption.Required);

            _logger.LogInformation($"invoice: {invoiceModel.Id} is {invoiceModel.EntityState}");

            switch (invoiceModel.EntityState)
            {
                case EntityStates.Loaded:
                    // nothing to do
                    
                    break;
                case EntityStates.New:
                    invoiceModel.Id = _db.GetNextId(); // Note that invoiceModel is dirty now!!!
                    InvoiceDTO invoiceDTO = InvoiceModel.ToDTO(
                        invoiceModel);
                    // INSERT
                    _db.Invoice.Add(
                        invoiceDTO.Id,
                        invoiceDTO);
                    break;

                case EntityStates.Updated:
                    invoiceDTO = InvoiceModel.ToDTO(
                        invoiceModel);
                    // UPDATE
                    _db.Invoice[invoiceDTO.Id] = invoiceDTO;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(invoiceModel.EntityState.ToString());
            }

            foreach (InvoiceItemModel itemModel in invoiceModel.Items)
            {
                _logger.LogInformation($"item: {itemModel.Id} is {itemModel.EntityState}");

                switch (itemModel.EntityState)
                {
                    case EntityStates.Loaded:
                        // Do nothing

                        break;
                    case EntityStates.New:
                        itemModel.Id = _db.GetNextId();
                        InvoiceItemDTO itemDTO = InvoiceItemModel.ToDTO(
                            itemModel);
                        // INSERT
                        _db.InvoiceItem.Add(
                            itemDTO.Id,
                            itemDTO);
                        break;

                    case EntityStates.Updated:
                        itemDTO = InvoiceItemModel.ToDTO(
                            itemModel);
                        // UPDATE
                        _db.InvoiceItem[itemDTO.Id] = itemDTO;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(itemModel.EntityState.ToString());
                }
            }

            ts.Complete();

            // reset EntityState of everything in repositiry
            invoiceModel.EntityState = EntityStates.Loaded;
            _logger.LogInformation($"Invoice: {invoiceModel.Id} is {invoiceModel.EntityState} on exit");

            foreach (InvoiceItemModel item in invoiceModel.Items)
            {
                item.EntityState = EntityStates.Loaded;

                _logger.LogInformation($"item: {item.Id} is {item.EntityState} on exit");
            }

            

        }

        public IEnumerable<GetAllInvoicesResponseItem> GetAll()
        {
            List<GetAllInvoicesResponseItem> responseItemList = new List<GetAllInvoicesResponseItem>();

            ILookup<int, InvoiceItemDTO> itemsGroupedByInvoiceId = _db.InvoiceItem.Values
                .ToLookup(p => p.InvoiceId, p => p);

            foreach (InvoiceDTO invoiceDto in _db.Invoice.Values)
            {
                IEnumerable<InvoiceItemDTO> itemsForInvoice = itemsGroupedByInvoiceId[invoiceDto.Id];

                GetAllInvoicesResponseItem responseItem = new GetAllInvoicesResponseItem(
                    Invoice: invoiceDto,
                    Items: itemsForInvoice);

                responseItemList.Add(responseItem);
            }

            return responseItemList;
        }
    }
}
