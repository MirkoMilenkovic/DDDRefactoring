using InvoiceWithDE.Invoice.DTO;

namespace InvoiceWithDE.Invoice.DomainEvents
{
    // TODO
    // Consider NOT using DTOs, so handlers can reference just the DomainEvent, not DTOs.
    // DomainEvents could then go to separate dll,
    // which would be the only way of communication between modules.
    public class InvoiceFinalizedDomainEventPayload
    {
        public required InvoiceDTO Invoice { get; init; }

        public required IEnumerable<InvoiceItemDTO> InvoiceItems { get; init; }

        public required DateTime DateOfFinalization { get; init; }
    }
}
