﻿using InvoiceWithTS.Inventory;
using InvoiceWithTS.Invoice.BusinessModel;
using InvoiceWithTS.TaxAdministration;
using System.Transactions;

namespace InvoiceWithTS.Invoice.UseCases.CancelInvoice
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

            if(invoiceModel.Status != DTO.InvoiceStatuses.Final)
            {
                throw new InvalidOperationException($"Invoice must be Final in order to become Canceled");
            }

            // apply change            
            invoiceModel.Status = DTO.InvoiceStatuses.Canceled;
            // oops. I've really forgotten this!!!
            invoiceModel.EntityState = Common.EntityStates.Updated;

            // start save
            using TransactionScope ts = new TransactionScope(TransactionScopeOption.Required);

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
            // oops. I've really forgotten this!!!

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
