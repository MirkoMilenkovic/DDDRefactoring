namespace InvoiceWithDDD.TaxAdministration
{
    public class TaxAdministrationDIConfigurator
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<TaxMessageRepository>();

            builder.Services.AddSingleton<TaxMessageCommonLogic>();
        }
    }
}
