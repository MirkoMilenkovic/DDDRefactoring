using InvoiceWithLayers.DB.DBModel;
using InvoiceWithLayers.MasterData.Articles;
using InvoiceWithLayers.MasterData.DBModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InvoiceWithLayers.DB
{
    /// <summary>
    /// Fake DB.
    /// </summary>
    public class InMemoryDB
    {
        private int _sequenceNumber = 0;

        public Dictionary<int, InvoiceDBT> Invoice { get; } = new Dictionary<int, InvoiceDBT>();

        public Dictionary<int, InvoiceItemDBT> InvoiceItem { get; } = new Dictionary<int, InvoiceItemDBT>();

        public Dictionary<int, ArticleDBT> Article { get; } = new Dictionary<int, ArticleDBT>();

        public Dictionary<int, CustomerDBT> Customer { get; } = new Dictionary<int, CustomerDBT>();

        public async Task Init()
        {
            InitArticles();

            CustomersInit();

            await Task.CompletedTask;
        }

        private void CustomersInit()
        {
            CustomerDBT customer = new CustomerDBT()
            {
                Id = GetNextId(),
                Name = "Maxi"
            };

            this.Customer.Add(
                customer.Id,
                customer);

            customer = new CustomerDBT()
            {
                Id = GetNextId(),
                Name = "Idea"
            };

            this.Customer.Add(
                customer.Id,
                customer);
        }

        private void InitArticles()
        {
            ArticleDBT article = new ArticleDBT()
            {
                ArticleTaxGroup = ArticleDBT.TaxGroup.Reduced,
                Code = "1",
                Name = "Jelen",
                UnitPriceWithoutTax = 100,
                Id = GetNextId()
            };

            this.Article.Add(
                article.Id,
                article);

            article = new ArticleDBT()
            {
                ArticleTaxGroup = ArticleDBT.TaxGroup.Reduced,
                Code = "2",
                Name = "Lav",
                UnitPriceWithoutTax = 90,
                Id = GetNextId()
            };

            this.Article.Add(
                article.Id,
                article);

            article = new ArticleDBT()
            {
                ArticleTaxGroup = ArticleDBT.TaxGroup.Reduced,
                Code = "3",
                Name = "Zajecarsko",
                UnitPriceWithoutTax = 80,
                Id = GetNextId()
            };

            this.Article.Add(
                article.Id,
                article);

            article = new ArticleDBT()
            {
                ArticleTaxGroup = ArticleDBT.TaxGroup.Normal,
                Code = "4",
                Name = "Budweiser",
                UnitPriceWithoutTax = 200,
                Id = GetNextId()
            };

            this.Article.Add(
                article.Id,
                article);

            article = new ArticleDBT()
            {
                ArticleTaxGroup = ArticleDBT.TaxGroup.Normal,
                Code = "5",
                Name = "Heineken",
                UnitPriceWithoutTax = 150,
                Id = GetNextId()
            };

            this.Article.Add(
                article.Id,
                article);
        }

        public int GetNextId()
        {
            return _sequenceNumber++;
        }
    }
}
