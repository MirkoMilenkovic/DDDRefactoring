using InvoiceWithDE.EventIInfrastructure;

namespace InvoiceWithDE.Inventory.EventHandlers
{
    public static class InventoryEventSubscriptionManager
    {
        /// <summary>
        /// Subsribes IDomainEventHandlers to eventBus
        /// </summary>
        public static void Subscribe(
            EventBus eventBus,
            IServiceProvider serviceProvider)
        {
            InvoiceFinalizedDomainEventHandler invoiceFinalizedHandler = serviceProvider.GetRequiredService<InvoiceFinalizedDomainEventHandler>();

            eventBus.Subscribe(invoiceFinalizedHandler);

            InvoiceCanceledDomainEventHandler invoiceCanceledHandler = serviceProvider.GetRequiredService<InvoiceCanceledDomainEventHandler>();

            eventBus.Subscribe(invoiceCanceledHandler);
        }
    }
}
