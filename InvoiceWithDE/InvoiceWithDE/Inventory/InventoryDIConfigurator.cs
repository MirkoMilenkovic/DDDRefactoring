namespace InvoiceWithDE.Inventory
{
    public static class InventoryDIConfigurator
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<InventoryItemRepository>();
        }
    }
}
