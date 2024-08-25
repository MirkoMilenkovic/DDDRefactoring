using InvoiceWithTS.TaxAdministration;

namespace InvoiceWithTS.MasterData.Articles
{
    public static class ArticlesDIConfigurator
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<ArticleRepository>();
        }
    }
}
