using InvoiceWithDE.Common;
using InvoiceWithDE.Invoice.DomainEvents;

namespace InvoiceWithDE.TaxAdministration.EventHandlers
{
    public class InvoiceFinalizedDomainEventHandler : IDomainEventHandler
    {
        private readonly ILogger<InvoiceFinalizedDomainEventHandler> _logger;

        private readonly TaxMessageCommonLogic _taxMessageCommonLogic;

        private readonly TaxMessageRepository _taxMessageRepository;

        public InvoiceFinalizedDomainEventHandler(
            ILogger<InvoiceFinalizedDomainEventHandler> logger,
            TaxMessageCommonLogic taxMessageCommonLogic,
            TaxMessageRepository taxMessageRepository)
        {
            _logger = logger;
            _taxMessageCommonLogic = taxMessageCommonLogic;
            _taxMessageRepository = taxMessageRepository;
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
            _logger.LogInformation($"TaxAdministration.{nameof(InvoiceFinalizedDomainEventHandler)}.Handle");

            InvoiceFinalizedDomainEvent? deTyped = de as InvoiceFinalizedDomainEvent;
            if (deTyped is null)
            {
                // a bug in handler subscription.
                throw new Exception($"{nameof(InvoiceFinalizedDomainEventHandler)} can not handle event of type {de.GetType().Name}");
            }

            InvoiceFinalizedDomainEventPayload payload = deTyped.Payload;

            TaxMessageInvoiceStatuses taxMessageInvoiceStatus = _taxMessageCommonLogic.MapInvoiceStatus(
                invoiceStatus: payload.Invoice.Status);

            TaxMessageDTO taxMessageDTO = new TaxMessageDTO()
            {
                CustomerId = payload.Invoice.CustomerId,
                InvoiceNumber = payload.Invoice.InvoiceNumber,
                PriceWithoutTax = payload.Invoice.PriceWithoutTax,
                PriceWithTax = payload.Invoice.PriceWithTax,
                TaxAtNormalRate = payload.Invoice.TaxAtNormalRate,
                TaxAtReducedRate = payload.Invoice.TaxAtReducedRate,
                Status = taxMessageInvoiceStatus,
            };

            _taxMessageRepository.EnqueueForSending(
                taxMessageDTO);
        }
    }
}
