using InvoiceWithDDD.Inventory;
using InvoiceWithDDD.Invoice.DTO;
using InvoiceWithDDD.MasterData.Articles;
using InvoiceWithDDD.MasterData.DBModel;
using InvoiceWithDDD.TaxAdministration;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InvoiceWithDDD.DB
{
    /// <summary>
    /// Fake DB.
    /// </summary>
    public class InMemoryDB
    {
        private int _sequenceNumber = 0;

        public Dictionary<int, InvoiceDTO> Invoice { get; } = new();

        public Dictionary<int, InvoiceItemDTO> InvoiceItem { get; } = new();

        public Dictionary<int, ArticleDTO> Article { get; } = new();

        public Dictionary<int, CustomerDTO> Customer { get; } = new();

        public Dictionary<int, InventoryItemDTO> InventoryItem { get; } = new();

        public Dictionary<int, TaxMessageDTO> TaxMessage { get; } = new();

        public async Task Init()
        {
            InitArticles();

            InitCustomers();

            InitInventory();

            await Task.CompletedTask;
        }

        private void InitCustomers()
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

        private void InitInventory()
        {
            foreach (ArticleDTO article in this.Article.Values)
            {
                InventoryItemDTO inventoryItem = new()
                {
                    ArticleId = article.Id,
                    Quantity = 10,
                    Id = GetNextId()
                };

                this.InventoryItem.Add(
                    inventoryItem.Id,
                    inventoryItem);
            }
        }

        public int GetNextId()
        {
            return _sequenceNumber++;
        }
    }
}
