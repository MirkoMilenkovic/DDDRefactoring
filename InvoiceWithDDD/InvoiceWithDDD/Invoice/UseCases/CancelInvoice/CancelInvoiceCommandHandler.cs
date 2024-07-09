using InvoiceWithDDD.Inventory;
using InvoiceWithDDD.Invoice.BusinessModel;
using System.Transactions;

namespace InvoiceWithDDD.Invoice.UseCases.CancelInvoice
{
    public class CancelInvoiceCommandHandler
    {
        private InvoiceRepository _invoiceRepo;

        private InventoryItemRepository _inventoryItemRepo;

        public CancelInvoiceCommandHandler(
            InvoiceRepository invoiceRepo, 
            InventoryItemRepository inventoryItemRepo)
        {
            _invoiceRepo = invoiceRepo;
            _inventoryItemRepo = inventoryItemRepo;
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

            // complete tran
            ts.Complete();

            return invoiceModel;
        }
    }
}
