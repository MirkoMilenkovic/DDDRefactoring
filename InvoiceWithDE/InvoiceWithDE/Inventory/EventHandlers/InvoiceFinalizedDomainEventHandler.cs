using InvoiceWithDE.Common;

namespace InvoiceWithDE.Inventory.EventHandlers
{
    public class InvoiceFinalizedDomainEventHandler : IDomainEventHandler
    {
        private ILogger<InvoiceFinalizedDomainEventHandler> _logger;

        public InvoiceFinalizedDomainEventHandler(
            ILogger<InvoiceFinalizedDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public DomainEventTypes EventType
        {
            get
            {
                return DomainEventTypes.InvoiceFinalized;
            }
        }

        public void Handle(BaseDomainEvent de)
        {
            // TODO
            // logic for TaxMessage moves here.
            _logger.LogError($"{nameof(InvoiceFinalizedDomainEventHandler)} call");
        }
    }
}
