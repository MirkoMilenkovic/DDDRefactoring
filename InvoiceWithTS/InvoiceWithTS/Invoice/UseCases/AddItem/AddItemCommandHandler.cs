using InvoiceWithTS.Common;
using InvoiceWithTS.Invoice.BusinessModel;
using InvoiceWithTS.MasterData.Articles;
using System.Transactions;

namespace InvoiceWithTS.Invoice.UseCases.AddItem
{
    public class AddItemCommandHandler
    {
        private InvoiceCommonLogic _commonLogic;

        private InvoiceRepository _invoiceRepo;

        private ArticleRepository _articleRepo;

        public AddItemCommandHandler(
            InvoiceCommonLogic commonLogic,
            InvoiceRepository invoiceRepo,
            ArticleRepository articleRepo)
        {
            _commonLogic = commonLogic;
            _invoiceRepo = invoiceRepo;
            _articleRepo = articleRepo;
        }

        public InvoiceModel AddItem(
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
                Tax = 0M, // Invalid
                TaxRate = 0M,
            };
            // at this moment, itemModel is invalid, because no tax

            // Calculate
            _commonLogic.CalculateMoney(
                itemModel,
                article);

            invoiceModel.Items.Add(itemModel);
            // right now, invoiceModel is invalid

            // Calculate
            _commonLogic.CalculateMoney(
                invoiceModel);

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
