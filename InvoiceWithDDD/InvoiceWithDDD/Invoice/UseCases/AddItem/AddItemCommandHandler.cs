using InvoiceWithDDD.Common;
using InvoiceWithDDD.Invoice.BusinessModel;
using InvoiceWithDDD.MasterData.Articles;
using System.Transactions;

namespace InvoiceWithDDD.Invoice.UseCases.AddItem
{
    public class AddItemCommandHandler
    {
        private InvoiceRepository _invoiceRepo;

        private ArticleRepository _articleRepo;

        public AddItemCommandHandler(
            InvoiceRepository invoiceRepo,
            ArticleRepository articleRepo)
        {
            _invoiceRepo = invoiceRepo;
            _articleRepo = articleRepo;
        }

        public (InvoiceModel Invoice, InvoiceItemModel AddedItem) AddItem(
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

            // DDD

            InvoiceItemModel itemModel = invoiceModel.AddItem(
                article: article,
                quantity: request.Quantity);

            /*
            InvoiceItemModel itemModel = new InvoiceItemModel()
            {
                // DDD
                // Id = 0, // We'll assign during Save 
                // This is not required anymore
                // END DDD
                EntityState = EntityStates.New,
                Invoice = invoiceModel,
                ArticleId = request.ArticleId,
                Quantity = request.Quantity,
                UnitPriceWithoutTax = 0M, // Invalid
                PriceWithoutTax = 0M, // Invalid
                PriceWithTax = 0M, // Invalid
                Tax = 0M, // Invalid
                TaxRate = 0M,
            };
            // at this moment, itemModel is invalid, because no tax

            // Calculate
            _commonLogic.CalculateMoney(
                item: itemModel,
                articleTaxGroup: article.ArticleTaxGroup,
                articleUnitPriceWithoutTax: article.UnitPriceWithoutTax);

            invoiceModel.Items.Add(itemModel);
            // right now, invoiceModel is invalid

            // Calculate
            // oops. If I forget this, Taxman comes!!!!
            _commonLogic.CalculateMoney(
                invoiceModel);
            */
            // END DDD

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
