using InvoiceWithTS.Inventory;
using InvoiceWithTS.Invoice.BusinessModel;
using System.Transactions;

namespace InvoiceWithTS.Invoice.UseCases.Finalize
{
    public class MakeFinalCommandHandler
    {
        private InvoiceRepository _invoiceRepo;

        private InventoryItemRepository _inventoryItemRepo;

        public MakeFinalCommandHandler(
            InvoiceRepository invoiceRepo,
            InventoryItemRepository inventoryItemRepo)
        {
            _invoiceRepo = invoiceRepo;
            _inventoryItemRepo = inventoryItemRepo;
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

            // complete tran
            ts.Complete();

            return invoiceModel;
        }
    }
}
