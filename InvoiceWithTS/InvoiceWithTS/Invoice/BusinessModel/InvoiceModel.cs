using InvoiceWithTS.Invoice.DTO;
using static InvoiceWithTS.Invoice.DTO.InvoiceDTO;

namespace InvoiceWithTS.Invoice.BusinessModel
{
    public class InvoiceModel
    {
        public required int Id { get; set; }

        public required string InvoiceNumber { get; set; }

        public required int CustomerId { get; set; }

        public required DateTime InvoiceDate { get; set; }

        public required InvoiceStatuses Status { get; set; }

        public required decimal PriceWithoutTax { get; set; } = 0M;

        public required decimal TaxAtNormalRate { get; set; } = 0M;

        public required decimal TaxAtReducedRate { get; set; } = 0M;

        public required decimal PriceWithTax { get; set; } = 0M;

        public List<InvoiceItemModel> Items { get; set; } = new List<InvoiceItemModel>();
    }
}
