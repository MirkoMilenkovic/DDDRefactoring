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

        public Dictionary<int, InvoiceDTO> Invoice { get; } = new Dictionary<int, InvoiceDTO>();

        public Dictionary<int, InvoiceItemDTO> InvoiceItem { get; } = new Dictionary<int, InvoiceItemDTO>();

        public Dictionary<int, ArticleDTO> Article { get; } = new Dictionary<int, ArticleDTO>();

        public Dictionary<int, CustomerDTO> Customer { get; } = new Dictionary<int, CustomerDTO>();

        public async Task Init()
        {
            InitArticles();

            CustomersInit();

            await Task.CompletedTask;
        }

        private void CustomersInit()
        {
            CustomerDTO customer = new CustomerDTO()
            {
                Id = GetNextId(),
                Name = "Maxi"
            };

            this.Customer.Add(
                customer.Id,
                customer);

            customer = new CustomerDTO()
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
            ArticleDTO article = new ArticleDTO()
            {
                ArticleTaxGroup = ArticleDTO.TaxGroup.Reduced,
                Code = "1",
                Name = "Jelen",
                UnitPriceWithoutTax = 100,
                Id = GetNextId()
            };

            this.Article.Add(
                article.Id,
                article);

            article = new ArticleDTO()
            {
                ArticleTaxGroup = ArticleDTO.TaxGroup.Reduced,
                Code = "2",
                Name = "Lav",
                UnitPriceWithoutTax = 90,
                Id = GetNextId()
            };

            this.Article.Add(
                article.Id,
                article);

            article = new ArticleDTO()
            {
                ArticleTaxGroup = ArticleDTO.TaxGroup.Reduced,
                Code = "3",
                Name = "Zajecarsko",
                UnitPriceWithoutTax = 80,
                Id = GetNextId()
            };

            this.Article.Add(
                article.Id,
                article);

            article = new ArticleDTO()
            {
                ArticleTaxGroup = ArticleDTO.TaxGroup.Normal,
                Code = "4",
                Name = "Budweiser",
                UnitPriceWithoutTax = 200,
                Id = GetNextId()
            };

            this.Article.Add(
                article.Id,
                article);

            article = new ArticleDTO()
            {
                ArticleTaxGroup = ArticleDTO.TaxGroup.Normal,
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
