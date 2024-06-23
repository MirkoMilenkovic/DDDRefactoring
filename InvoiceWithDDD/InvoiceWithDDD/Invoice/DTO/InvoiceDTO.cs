using InvoiceWithDDD.Common;

namespace InvoiceWithDDD.Invoice.DTO
{
    /// <summary>
    /// Maps to Invoice table.
    /// </summary>
    public class InvoiceDTO : BaseDTO
    {
        public required string InvoiceNumber { get; set; }

        public required int CustomerId { get; set; }

        public required DateTime InvoiceDate { get; set; }

        /// <summary>
        /// varchar column with CHECK constraint
        /// </summary>
        public required InvoiceStatuses Status { get; set; }

        public required decimal PriceWithoutTax { get; set; } = 0M;

        public required decimal TaxAtNormalRate { get; set; } = 0M;

        public required decimal TaxAtReducedRate { get; set; } = 0M;

        public required decimal PriceWithTax { get; set; } = 0M;

    }
}
