using InvoiceWithDE.TaxAdministration.EventHandlers;

namespace InvoiceWithDE.TaxAdministration
{
    public class TaxAdministrationDIConfigurator
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<TaxMessageRepository>();

            builder.Services.AddSingleton<TaxMessageCommonLogic>();

            builder.Services.AddSingleton<InvoiceFinalizedDomainEventHandler>();

            builder.Services.AddSingleton<InvoiceCanceledDomainEventHandler>();
        }
    }
}
