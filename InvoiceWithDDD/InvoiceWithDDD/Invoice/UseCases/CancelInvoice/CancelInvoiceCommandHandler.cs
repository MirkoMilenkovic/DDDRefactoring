using InvoiceWithDDD.Inventory;
using InvoiceWithDDD.Invoice.BusinessModel;
using InvoiceWithDDD.TaxAdministration;
using System.Transactions;

namespace InvoiceWithDDD.Invoice.UseCases.CancelInvoice
{
    public class CancelInvoiceCommandHandler
    {
        private readonly InvoiceRepository _invoiceRepo;

        private readonly InventoryItemRepository _inventoryItemRepo;

        private readonly TaxMessageCommonLogic _taxMessageCommonLogic;

        private readonly TaxMessageRepository _taxMessageRepository;

        public CancelInvoiceCommandHandler(
            InvoiceRepository invoiceRepo,
            InventoryItemRepository inventoryItemRepo,
            TaxMessageCommonLogic taxMessageCommonLogic,
            TaxMessageRepository taxMessageRepository)
        {
            _invoiceRepo = invoiceRepo;
            _inventoryItemRepo = inventoryItemRepo;
            _taxMessageCommonLogic = taxMessageCommonLogic;
            _taxMessageRepository = taxMessageRepository;
        }

        public InvoiceModel Cancel(CancelInvoiceCommand request)
        {
            InvoiceModel? invoiceModel = _invoiceRepo.GetById(
               request.InvoiceId);

            if (invoiceModel == null)
            {
                throw new Exception($"{request.InvoiceId} not found");
            }

            // DDD

            invoiceModel.Cancel();

            /*
            if(invoiceModel.Status != DTO.InvoiceStatuses.Final)
            {
                throw new InvalidOperationException($"Invoice must be Final in order to become Canceled");
            }

            // apply change            
            invoiceModel.Status = DTO.InvoiceStatuses.Canceled;
            */
            // I've just realized that we have forgot to update EntityState!!!
            // test would have caught it, but I was to lazy to write a test.

            // END DDD


            // start save
            using TransactionScope ts = new TransactionScope();

            // Save invoice
            _invoiceRepo.Save(
                invoiceModel);

            // reduce inventory
            // oops
            // If I forget this, we will have ghost items in warehouse
            foreach (InvoiceItemModel item in invoiceModel.Items)
            {
                _inventoryItemRepo.IncreaseQuantity(
                    articleId: item.ArticleId,
                    quantity: item.Quantity);
            }

            // send message to Taxman
            // oops. I've really forgoten this!!!

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
