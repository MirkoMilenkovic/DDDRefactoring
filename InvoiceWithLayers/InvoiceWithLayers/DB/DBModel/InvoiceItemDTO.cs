using InvoiceWithLayers.Common;

namespace InvoiceWithLayers.DB.DBModel
{
    /// <summary>
    /// Maps to InvoiceItem table.
    /// </summary>
    public class InvoiceItemDTO : BaseDBT
    {
        public required int InvoiceId { get; set; }

        public required int ArticleId { get; set; }

        public required int Quantity { get; set; }

        public required decimal PriceWithoutTax { get; set; }

        public required decimal Tax { get; set; }

        public required decimal PriceWithTax { get; set; }
    }
}
