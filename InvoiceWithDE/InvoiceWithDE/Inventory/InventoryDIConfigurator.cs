using InvoiceWithDE.Inventory.EventHandlers;

namespace InvoiceWithDE.Inventory
{
    public static class InventoryDIConfigurator
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<InventoryItemRepository>();

            builder.Services.AddSingleton<InvoiceFinalizedDomainEventHandler>();

            builder.Services.AddSingleton<InvoiceCanceledDomainEventHandler>();
        }
    }
}
