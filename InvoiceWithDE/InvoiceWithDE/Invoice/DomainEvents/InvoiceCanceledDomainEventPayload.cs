using InvoiceWithDE.Invoice.DTO;

namespace InvoiceWithDE.Invoice.DomainEvents
{
    public class InvoiceCanceledDomainEventPayload
    {
        public required InvoiceDTO Invoice { get; init; }

        public required IEnumerable<InvoiceItemDTO> InvoiceItems { get; init; }

        public required DateTime DateOfCancelation { get; init; }
    }
}
