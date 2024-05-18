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
        private const decimal TAX_RATE_NORMAL = .2M;

        private const decimal TAX_RATE_REDUCED = .1M;

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


        /// <summary>
        /// Mutates item.
        /// </summary>
        public void CalculateMoney(
            InvoiceItemModel item,
            ArticleDTO article)
        {
            item.PriceWithoutTax = article.UnitPriceWithoutTax * item.Quantity;

            switch (article.ArticleTaxGroup)
            {
                case ArticleDTO.TaxGroup.Normal:
                    item.TaxRate = TAX_RATE_NORMAL;
                    break;
                case ArticleDTO.TaxGroup.Reduced:
                    item.TaxRate = TAX_RATE_REDUCED;
                    break;
                default:
                    throw new Exception($"Tax man came up with unexpected tax group: {article.ArticleTaxGroup}");
            }

            item.Tax = item.TaxRate * item.PriceWithoutTax;

            item.PriceWithTax = item.PriceWithoutTax + item.Tax;
        }

        public void CalculateMoney(
           InvoiceModel invoice)
        {
            // reset
            invoice.PriceWithoutTax = 0M;
            invoice.PriceWithTax = 0M;
            invoice.TaxAtNormalRate = 0M;
            invoice.TaxAtReducedRate = 0M;

            foreach (InvoiceItemModel item in invoice.Items)
            {
                invoice.PriceWithoutTax += item.PriceWithoutTax;
                if (item.TaxRate == TAX_RATE_NORMAL)
                {
                    invoice.TaxAtNormalRate += item.Tax;
                }
                else if (item.TaxRate == TAX_RATE_REDUCED)
                {
                    invoice.TaxAtReducedRate += item.Tax;
                }
            }

            invoice.PriceWithTax = invoice.PriceWithoutTax
                + invoice.TaxAtReducedRate
                + invoice.TaxAtNormalRate;
        }
    }
}
