using InvoiceWithDE.Common;

namespace InvoiceWithDE.Invoice.DTO
{
    /// <summary>
    /// Maps to InvoiceItem table.
    /// </summary>
    public class InvoiceItemDTO : BaseDTO
    {
        public required int InvoiceId { get; set; }

        public required int ArticleId { get; set; }

        public required int Quantity { get; set; }

        public required decimal UnitPriceWithoutTax { get; set; }

        public required decimal PriceWithoutTax { get; set; }

        public required decimal Tax { get; set; }

        public required decimal PriceWithTax { get; set; }

        public required decimal TaxRate { get; set; }
    }
}
