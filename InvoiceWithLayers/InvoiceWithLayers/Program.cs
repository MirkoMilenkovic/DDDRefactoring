using InvoiceWithLayers.DB;
using InvoiceWithLayers.DB.DBModel;
using InvoiceWithLayers.MasterData.Articles;
using InvoiceWithLayers.MasterData.Customers;
using InvoiceWithLayers.MasterData.DBModel;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWithLayers
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            ConfigureDI(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            ConfigureMinimalApi(app);

            var db = app.Services.GetRequiredService<InMemoryDB>();

            await db.Init();

            await app.RunAsync();
        }

        private static void ConfigureDI(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<InMemoryDB>();

            builder.Services.AddSingleton<ArticleRepository>();
            builder.Services.AddSingleton<ArticleManager>();

            builder.Services.AddSingleton<CustomerRepository>();
            builder.Services.AddSingleton<CustomerManager>();
        }

        
        private static void ConfigureMinimalApi(WebApplication app)
        {
            app.ConfigureCustomerMinimalApi();

            app.ConfigureArticleMinimalApi();
        }

    }
}
