using InvoiceWithDE.Common;
using InvoiceWithDE.Invoice.DomainEvents;
using InvoiceWithDE.Invoice.DTO;
using System.Transactions;

namespace InvoiceWithDE.Inventory.EventHandlers
{
    public class InvoiceCanceledDomainEventHandler : IDomainEventHandler
    {
        private ILogger<InvoiceCanceledDomainEventHandler> _logger;

        private InventoryItemRepository _inventoryItemRepo;

        public InvoiceCanceledDomainEventHandler(
            ILogger<InvoiceCanceledDomainEventHandler> logger, 
            InventoryItemRepository inventoryItemRepo)
        {
            _logger = logger;
            _inventoryItemRepo = inventoryItemRepo;
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
            _logger.LogInformation($"Inventory.{nameof(InvoiceCanceledDomainEventHandler)}.Handle");

            InvoiceCanceledDomainEvent? deTyped = de as InvoiceCanceledDomainEvent;
            if (deTyped is null)
            {
                // a bug in handler subscription.
                throw new Exception($"{nameof(InvoiceCanceledDomainEventHandler)} can not handle event of type {de.GetType().Name}");
            }

            InvoiceCanceledDomainEventPayload payload = deTyped.Payload;

            // We want to join ambient transaction!!!!
            // If there is no transaction, start one.

            using TransactionScope ts = new TransactionScope(TransactionScopeOption.Required);

            foreach (InvoiceItemDTO item in payload.InvoiceItems)
            {
                _inventoryItemRepo.IncreaseQuantity(
                    articleId: item.ArticleId,
                    quantity: item.Quantity);
            }

            // this is "fake commit",
            // ambient transaction will be committed where it started
            ts.Complete();
        }
    }
}
