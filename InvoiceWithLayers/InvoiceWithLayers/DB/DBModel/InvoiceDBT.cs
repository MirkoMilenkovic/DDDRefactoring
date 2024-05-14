using InvoiceWithLayers.Common;

namespace InvoiceWithLayers.DB.DBModel
{
    /// <summary>
    /// Maps to Invoice table.
    /// </summary>
    public class InvoiceDBT : BaseDBT
    {
        public enum Status
        {
            Draft,
            Final,
            Canceled
        }

        public  required int CustomerId { get; set; }

        public required DateTime InvoiceDate { get; set; }

        /// <summary>
        /// varchar column with CHECK constraint
        /// </summary>
        public required Status InvoiceStatus { get; set; }

        public required decimal PriceWithoutTax { get; set; } = 0M;

        public required decimal TaxAtNormalRate { get; set; } = 0M;

        public required decimal TaxAtReducedRate { get; set; } = 0M;

        public required decimal PriceWithTax { get; set; } = 0M;

    }
}
