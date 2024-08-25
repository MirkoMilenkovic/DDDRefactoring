namespace InvoiceWithDDD.MasterData.Customers
{
    public static class CustomersDIConfigurator
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<CustomerRepository>();
        }
    }
}
