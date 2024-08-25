using InvoiceWithDE.Common;
using InvoiceWithDE.Invoice.BusinessModel;
using InvoiceWithDE.Invoice.DomainEvents;
using InvoiceWithDE.Invoice.DTO;
using System.Transactions;

namespace InvoiceWithDE.Inventory.EventHandlers
{
    public class InvoiceFinalizedDomainEventHandler : IDomainEventHandler
    {
        private ILogger<InvoiceFinalizedDomainEventHandler> _logger;

        private readonly InventoryItemRepository _inventoryItemRepo;

        public InvoiceFinalizedDomainEventHandler(
            ILogger<InvoiceFinalizedDomainEventHandler> logger,
            InventoryItemRepository inventoryItemRepo)
        {
            _logger = logger;
            _inventoryItemRepo = inventoryItemRepo;
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
            _logger.LogInformation($"Inventory.{nameof(InvoiceFinalizedDomainEventHandler)}.Handle");

            InvoiceFinalizedDomainEvent? deTyped = de as InvoiceFinalizedDomainEvent;
            if (deTyped is null)
            {
                // a bug in handler subscription.
                throw new Exception($"{nameof(InvoiceFinalizedDomainEventHandler)} can not handle event of type {de.GetType().Name}");
            }

            InvoiceFinalizedDomainEventPayload payload = deTyped.Payload;

            // We want to join ambient transaction!!!!
            // If there is no transaction, start one.

            using TransactionScope ts = new TransactionScope(TransactionScopeOption.Required);

            foreach (InvoiceItemDTO item in payload.InvoiceItems)
            {
                _inventoryItemRepo.ReduceQuantity(
                    articleId: item.ArticleId,
                    quantity: item.Quantity);
            }

            // this is "fake commit",
            // ambient transaction will be committed where it started
            ts.Complete();
        }
    }
}
