using InvoiceWithTS.MasterData.Articles;

namespace InvoiceWithTS.MasterData.Customers
{
    public static class CustomersDIConfigurator
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<CustomerRepository>();
        }
    }
}
