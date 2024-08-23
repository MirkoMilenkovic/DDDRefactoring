using InvoiceWithDDD.Inventory;
using InvoiceWithDDD.Invoice.BusinessModel;
using InvoiceWithDDD.TaxAdministration;
using System.Transactions;

namespace InvoiceWithDDD.Invoice.UseCases.Finalize
{
    public class MakeFinalCommandHandler
    {
        private readonly InvoiceRepository _invoiceRepo;

        private readonly InventoryItemRepository _inventoryItemRepo;

        private readonly TaxMessageRepository _taxMessageRepository;

        private readonly TaxMessageCommonLogic _taxMessageCommonLogic;

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

            // DDD
            // The code below remains a problem,
            // because I have to know details of Tax system and Inventory.
            // We will try to solve it for next release.    

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
                Status = taxMessageInvoiceStatus
            };

            _taxMessageRepository.EnqueueForSending(
                taxMessageDTO);

            // complete tran
            ts.Complete();

            return invoiceModel;
        }
    }
}
