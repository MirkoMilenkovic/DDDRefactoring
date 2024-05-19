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
    public class InvoiceCommonLogic
    {
        private const decimal TAX_RATE_NORMAL = .2M;

        private const decimal TAX_RATE_REDUCED = .1M;


        /// <summary>
        /// Mutates item.
        /// </summary>
        // Note that I'm not sending Article, but its properties.
        public void CalculateMoney(
            InvoiceItemModel item,
            ArticleDTO.TaxGroup articleTaxGroup,
            decimal articleUnitPriceWithoutTax)
        {
            switch (articleTaxGroup)
            {
                case ArticleDTO.TaxGroup.Normal:
                    item.TaxRate = TAX_RATE_NORMAL;
                    break;
                case ArticleDTO.TaxGroup.Reduced:
                    item.TaxRate = TAX_RATE_REDUCED;
                    break;
                default:
                    throw new Exception($"Tax man came up with unexpected tax group: {articleTaxGroup}");
            }

            item.UnitPriceWithoutTax = articleUnitPriceWithoutTax;

            CalculateMoney(item);
        }

        public void CalculateMoney(
            InvoiceItemModel item)
        {
            item.PriceWithoutTax = item.UnitPriceWithoutTax * item.Quantity;

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
