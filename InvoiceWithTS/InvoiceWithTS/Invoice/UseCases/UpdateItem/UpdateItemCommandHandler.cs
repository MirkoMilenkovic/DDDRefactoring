﻿using InvoiceWithTS.Common;
using InvoiceWithTS.Invoice.BusinessModel;
using InvoiceWithTS.MasterData.Articles;
using System.Transactions;

namespace InvoiceWithTS.Invoice.UseCases.UpdateItem
{
    public class UpdateItemCommandHandler
    {
        private InvoiceCommonLogic _commonLogic;

        private InvoiceRepository _invoiceRepo;

        public UpdateItemCommandHandler(
            InvoiceCommonLogic commonLogic,
            InvoiceRepository invoiceRepo)
        {
            _commonLogic = commonLogic;
            _invoiceRepo = invoiceRepo;
        }

        public (InvoiceModel Invoice, InvoiceItemModel UpdatedItem) UpdateItem(
            UpdateItemCommand request)
        {
            InvoiceModel? invoiceModel = _invoiceRepo.GetById(
                request.InvoiceId);

            if (invoiceModel == null)
            {
                throw new Exception($"Invoice {request.InvoiceId} not found");
            }

            InvoiceItemModel? itemModel = invoiceModel.Items
                .FirstOrDefault(x => x.Id == request.ItemId);

            if (itemModel == null)
            {
                throw new Exception($"Item {request.ItemId} not found");
            }

            itemModel.Quantity = request.Quantity;

            // Do not forget this!!!
            itemModel.EntityState = EntityStates.Updated;

            // at this moment, itemModel is invalid, because tax is wrong

            // Calculate Item
            // oops. If I forget this, Taxman comes!!!!
            _commonLogic.CalculateMoney(
                itemModel);

            // Calculate Invoice
            // oops. If I forget this, Taxman comes!!!!
            _commonLogic.CalculateMoney(
                invoiceModel);


            // start save
            using TransactionScope ts = new TransactionScope(TransactionScopeOption.Required);

            // Save invoice
            _invoiceRepo.Save(
                invoiceModel);

            // complete tran
            ts.Complete();

            return (invoiceModel, itemModel);
        }
    }
}
