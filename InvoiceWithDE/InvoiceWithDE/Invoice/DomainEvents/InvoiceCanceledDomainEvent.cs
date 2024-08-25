using InvoiceWithDE.Invoice.DTO;

namespace InvoiceWithDE.Invoice.DomainEvents
{
    public class InvoiceCanceledDomainEvent
    {
        public required InvoiceDTO Invoice { get; init; }

        public required IReadOnlyList<InvoiceItemDTO> InvoiceItems { get; init; }

        public required DateTime DateOfCancelation { get; init; }
    }
}
