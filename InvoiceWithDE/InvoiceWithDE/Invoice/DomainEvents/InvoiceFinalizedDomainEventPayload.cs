using InvoiceWithDE.Invoice.DTO;

namespace InvoiceWithDE.Invoice.DomainEvents
{
    public class InvoiceFinalizedDomainEventPayload
    {
        public required InvoiceDTO Invoice { get; init; }

        public required IEnumerable<InvoiceItemDTO> InvoiceItems { get; init; }

        public required DateTime DateOfFinalization { get; init; }
    }
}
