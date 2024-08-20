using InvoiceWithDDD.Common;

namespace InvoiceWithDDD.TaxAdministration
{
    public class TaxMessageDTO : BaseDTO
    {
        public required string InvoiceNumber { get; init; }

        public required int CustomerId { get; init; }

        public required decimal PriceWithoutTax { get; init; } = 0M;

        public required decimal TaxAtNormalRate { get; init; } = 0M;

        public required decimal TaxAtReducedRate { get; init; } = 0M;

        public required decimal PriceWithTax { get; init; } = 0M;

        public required TaxMessageInvoiceStatuses Status { get; init; }
    }
}
