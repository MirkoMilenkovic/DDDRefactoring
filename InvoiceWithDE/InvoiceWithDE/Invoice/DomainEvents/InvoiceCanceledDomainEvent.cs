using InvoiceWithDE.Common;
using InvoiceWithDE.Invoice.DTO;

namespace InvoiceWithDE.Invoice.DomainEvents
{
    public class InvoiceCanceledDomainEvent : BaseDomainEvent<InvoiceCanceledDomainEventPayload>
    {
        public InvoiceCanceledDomainEvent(
            InvoiceCanceledDomainEventPayload payload) : base(payload)
        {
        }

        public override DomainEventTypes EventType
        {
            get
            {
                return DomainEventTypes.InvoiceCanceled;
            }
        }
    }
}