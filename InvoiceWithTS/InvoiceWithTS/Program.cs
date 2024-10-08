﻿using InvoiceWithTS.DB;
using InvoiceWithTS.Inventory;
using InvoiceWithTS.Inventory.GetInventory;
using InvoiceWithTS.Invoice;
using InvoiceWithTS.Invoice.UseCases.AddItem;
using InvoiceWithTS.Invoice.UseCases.CancelInvoice;
using InvoiceWithTS.Invoice.UseCases.CreateInvoice;
using InvoiceWithTS.Invoice.UseCases.Finalize;
using InvoiceWithTS.Invoice.UseCases.GetAllInvoices;
using InvoiceWithTS.Invoice.UseCases.UpdateItem;
using InvoiceWithTS.MasterData.Articles;
using InvoiceWithTS.MasterData.Customers;
using InvoiceWithTS.TaxAdministration;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvoiceWithTS
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddLogging(builder => 
                builder.AddFilter("Microsoft", LogLevel.Warning));
            //builder.Logging.ClearProviders();
            
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();            

            builder.Services.AddControllers();
            builder.Services.Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

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

            ArticlesDIConfigurator.Configure(builder);

            CustomersDIConfigurator.Configure(builder);

            InvoiceDIConfigurator.Configure(builder);

            TaxAdministrationDIConfigurator.Configure(builder);

            InventoryDIConfigurator.Configure(builder);
        }

        private static void ConfigureMinimalApi(WebApplication app)
        {
            app.ConfigureCustomerMinimalApi();

            app.ConfigureArticleMinimalApi();

            app.ConfigureCreateInvoiceMinimalApi();

            app.ConfigureGetAllInvoicesMinimalApi();

            app.ConfigureAddItemMinimalApi();

            app.ConfigureUpdateItemMinimalApi();

            app.ConfigureMakeFinalMinimalApi();

            app.ConfigureGetInventoryMinimalApi();

            app.ConfigureCancelInvoiceMinimalApi();

            app.ConfigureTaxMessageMinimalApi();
        }
    }
}
