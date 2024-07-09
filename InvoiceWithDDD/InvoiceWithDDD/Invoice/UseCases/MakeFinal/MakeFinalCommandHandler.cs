using InvoiceWithDDD.Inventory;
using InvoiceWithDDD.Invoice.BusinessModel;
using InvoiceWithDDD.TaxAdministration;
using System.Transactions;

namespace InvoiceWithDDD.Invoice.UseCases.Finalize
{
    public class MakeFinalCommandHandler
    {
        private InvoiceRepository _invoiceRepo;

        private InventoryItemRepository _inventoryItemRepo;

        private TaxMessageRepository _taxMessageRepository;

        public MakeFinalCommandHandler(
            InvoiceRepository invoiceRepo,
            InventoryItemRepository inventoryItemRepo,
            TaxMessageRepository taxMessageRepository)
        {
            _invoiceRepo = invoiceRepo;
            _inventoryItemRepo = inventoryItemRepo;
            _taxMessageRepository = taxMessageRepository;
        }

        public InvoiceModel MakeFinal(MakeFinalCommand request)
        {
            InvoiceModel? invoiceModel = _invoiceRepo.GetById(
               request.InvoiceId);

            if (invoiceModel == null)
            {
                throw new Exception($"{request.InvoiceId} not found");
            }

            // DDD

            invoiceModel.MakeFinal();

            // apply change        
            /*
            invoiceModel.Status = DTO.InvoiceStatuses.Final;

            // Do not forget this.
            invoiceModel.EntityState = Common.EntityStates.Updated;
            */
            // END DDD

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

            // DDD
            // The code below remains a problem,
            // because I have to know details of Tax system and Inventory.
            // We will try to solve it for next release.            
            
            // Do not forget!!!
            // Taxman will come!!!
            TaxMessageDTO taxMessageDTO = new TaxMessageDTO()
            {
                CustomerId = invoiceModel.CustomerId,
                InvoiceNumber = invoiceModel.InvoiceNumber,
                PriceWithoutTax = invoiceModel.PriceWithoutTax,
                PriceWithTax = invoiceModel.PriceWithTax,
                TaxAtNormalRate = invoiceModel.TaxAtNormalRate,
                TaxAtReducedRate = invoiceModel.TaxAtReducedRate,
            };

            _taxMessageRepository.EnqueueForSending(
                taxMessageDTO);

            // complete tran
            ts.Complete();

            return invoiceModel;
        }
    }
}
