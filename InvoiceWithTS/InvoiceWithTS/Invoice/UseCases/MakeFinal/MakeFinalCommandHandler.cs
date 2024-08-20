using InvoiceWithTS.Inventory;
using InvoiceWithTS.Invoice.BusinessModel;
using InvoiceWithTS.TaxAdministration;
using System.Transactions;

namespace InvoiceWithTS.Invoice.UseCases.Finalize
{
    public class MakeFinalCommandHandler
    {
        private InvoiceRepository _invoiceRepo;

        private InventoryItemRepository _inventoryItemRepo;

        private TaxMessageRepository _taxMessageRepository;

        private TaxMessageCommonLogic _taxMessageCommonLogic;

        public MakeFinalCommandHandler(
            InvoiceRepository invoiceRepo,
            InventoryItemRepository inventoryItemRepo,
            TaxMessageRepository taxMessageRepository,
            TaxMessageCommonLogic taxMessageCommonLogic)
        {
            _invoiceRepo = invoiceRepo;
            _inventoryItemRepo = inventoryItemRepo;
            _taxMessageRepository = taxMessageRepository;
            _taxMessageCommonLogic = taxMessageCommonLogic;
        }

        public InvoiceModel MakeFinal(MakeFinalCommand request)
        {
            InvoiceModel? invoiceModel = _invoiceRepo.GetById(
               request.InvoiceId);

            if (invoiceModel == null)
            {
                throw new Exception($"{request.InvoiceId} not found");
            }

            // apply change            
            invoiceModel.Status = DTO.InvoiceStatuses.Final;

            // Do not forget this.
            invoiceModel.EntityState = Common.EntityStates.Updated;

            // start save
            using TransactionScope ts = new TransactionScope();

            // Save invoice
            _invoiceRepo.Save(
                invoiceModel);

            //reduce inventory
            foreach (InvoiceItemModel item in invoiceModel.Items)
            {
                _inventoryItemRepo.ReduceQuantity(
                    articleId: item.ArticleId,
                    quantity: item.Quantity);
            }

            // Do not forget!!!
            // Taxman will come!!!
            TaxMessageInvoiceStatuses taxMessageInvoiceStatus = _taxMessageCommonLogic.MapInvoiceStatus(
                invoiceStatus: invoiceModel.Status);

            TaxMessageDTO taxMessageDTO = new TaxMessageDTO()
            {
                CustomerId = invoiceModel.CustomerId,
                InvoiceNumber = invoiceModel.InvoiceNumber,
                PriceWithoutTax = invoiceModel.PriceWithoutTax,
                PriceWithTax = invoiceModel.PriceWithTax,
                TaxAtNormalRate = invoiceModel.TaxAtNormalRate,
                TaxAtReducedRate = invoiceModel.TaxAtReducedRate,
                Status = taxMessageInvoiceStatus,
            };

            _taxMessageRepository.EnqueueForSending(
                taxMessageDTO);

            // complete tran
            ts.Complete();

            return invoiceModel;
        }
    }
}
