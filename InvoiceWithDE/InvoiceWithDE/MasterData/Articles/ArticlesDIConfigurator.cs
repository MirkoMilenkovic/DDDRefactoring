namespace InvoiceWithDE.MasterData.Articles
{
    public static class ArticlesDIConfigurator
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<ArticleRepository>();
        }
    }
}
