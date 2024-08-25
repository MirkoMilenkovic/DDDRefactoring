using InvoiceWithDE.Common;
using InvoiceWithDE.Invoice.DTO;

namespace InvoiceWithDE.Invoice.DomainEvents
{
    public class InvoiceFinalizedDomainEvent : BaseDomainEvent<InvoiceFinalizedDomainEventPayload>
    {
        public InvoiceFinalizedDomainEvent(
            InvoiceFinalizedDomainEventPayload payload) 
            : base(payload)
        {
        }

        public override DomainEventTypes EventType
        {
            get
            {
                return DomainEventTypes.InvoiceFinalized;
            }
        }
    }
}
