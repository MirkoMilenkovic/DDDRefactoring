﻿using InvoiceWithTS.Inventory;
using InvoiceWithTS.Invoice.BusinessModel;
using System.Transactions;

namespace InvoiceWithTS.Invoice.UseCases.CancelInvoice
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

            if(invoiceModel.Status != DTO.InvoiceStatuses.Final)
            {
                throw new InvalidOperationException($"Invoice must be Final in order to become Canceled");
            }

            // apply change            
            invoiceModel.Status = DTO.InvoiceStatuses.Canceled;

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
