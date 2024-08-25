using InvoiceWithDE.Common;

namespace InvoiceWithDE.TaxAdministration.EventHandlers
{
    public class InvoiceCanceledDomainEventHandler : IDomainEventHandler
    {
        private ILogger<InvoiceCanceledDomainEventHandler> _logger;

        public InvoiceCanceledDomainEventHandler(
            ILogger<InvoiceCanceledDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public DomainEventTypes EventType
        {
            get
            {
                return DomainEventTypes.InvoiceCanceled;
            }
        }

        public void Handle(BaseDomainEvent de)
        {
            // TODO
            // logic for TaxMessage moves here.
            _logger.LogError($"{nameof(InvoiceCanceledDomainEventHandler)} call");
        }
    }
}
