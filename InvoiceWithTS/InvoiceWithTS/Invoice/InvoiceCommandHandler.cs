using InvoiceWithTS.Common;
using InvoiceWithTS.Invoice.BusinessModel;
using InvoiceWithTS.Invoice.DTO;
using InvoiceWithTS.Invoice.UseCases.AddItem;
using InvoiceWithTS.Invoice.UseCases.CreateInvoice;
using InvoiceWithTS.MasterData.Articles;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Transactions;

namespace InvoiceWithTS.Invoice
{
    public class InvoiceCommandHandler
    {
        private InvoiceRepository _invoiceRepo;

        private ArticleRepository _articleRepo;

        public InvoiceCommandHandler(
            InvoiceRepository repo,
            ArticleRepository articleRepo)
        {
            _invoiceRepo = repo;
            _articleRepo = articleRepo;
        }

        /// <summary>
        /// Returns new Invoice.
        /// </summary>
        public InvoiceModel Create(CreateInvoiceCommand command)
        {
            // Business logic for new invoice follows...

            InvoiceModel invoiceModel = new InvoiceModel()
            {
                EntityState = Common.EntityStates.New, // we are creating New
                Id = 0, // DB will assign
                CustomerId = command.CustomerId, // by user
                InvoiceNumber = command.InvoiceNumber, // by user
                InvoiceDate = DateTime.Today, // business rule
                Status = InvoiceStatuses.Draft, // business rule
                PriceWithoutTax = 0, // business rule
                PriceWithTax = 0, // business rule
                TaxAtNormalRate = 0, // business rule
                TaxAtReducedRate = 0, // business rule
            };

            _invoiceRepo.Save(
                invoiceModel);

            // Id will be assigned
            return invoiceModel;
        }

        internal InvoiceModel AddItem(
            AddItemCommand request)
        {
            InvoiceModel? invoiceModel = _invoiceRepo.GetById(
                request.InvoiceId);

            if (invoiceModel == null)
            {
                throw new Exception($"{request.InvoiceId} not found");
            }

            ArticleDTO? article = _articleRepo.GetById(
                request.ArticleId);

            if (article == null)
            {
                throw new Exception($"Article: {request.ArticleId} not found");
            }

            InvoiceItemModel itemModel = new InvoiceItemModel()
            {
                Id = 0, // We'll assign during Save 
                EntityState = EntityStates.New,
                Invoice = invoiceModel,
                ArticleId = request.ArticleId,
                Quantity = request.Quantity,
                PriceWithoutTax = 0M, // Invalid
                PriceWithTax = 0M, // Invalid
                Tax = 0M // Invalid
            };
            // at this moment, itemModel is invalid, because no tax
            
            invoiceModel.Items.Add(itemModel);
            // right now, invoiceModel is invalid

            // start save
            using TransactionScope ts = new TransactionScope();

            // Save invoice
            _invoiceRepo.Save(
                invoiceModel);

            // complete tran
            ts.Complete();

            return invoiceModel;
        }
    }
}
