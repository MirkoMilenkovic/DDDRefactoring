using InvoiceWithDE.Common;
using InvoiceWithDE.Invoice.BusinessModel;
using InvoiceWithDE.Invoice.DomainEvents;

namespace InvoiceWithDE.TaxAdministration.EventHandlers
{
    public class InvoiceCanceledDomainEventHandler : IDomainEventHandler
    {
        private ILogger<InvoiceCanceledDomainEventHandler> _logger;

        private readonly TaxMessageCommonLogic _taxMessageCommonLogic;

        private readonly TaxMessageRepository _taxMessageRepository;

        public InvoiceCanceledDomainEventHandler(
            ILogger<InvoiceCanceledDomainEventHandler> logger, 
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
                return DomainEventTypes.InvoiceCanceled;
            }
        }

        public void Handle(BaseDomainEvent de)
        {
            _logger.LogInformation($"TaxAdministration.{nameof(InvoiceCanceledDomainEventHandler)}.Handle");

            InvoiceCanceledDomainEvent? deTyped = de as InvoiceCanceledDomainEvent;
            if (deTyped is null)
            {
                // a bug in handler subscription.
                throw new Exception($"{nameof(InvoiceCanceledDomainEventHandler)} can not handle event of type {de.GetType().Name}");
            }

            InvoiceCanceledDomainEventPayload payload = deTyped.Payload;

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
