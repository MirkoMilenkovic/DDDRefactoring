﻿using InvoiceWithDDD.DB;
using InvoiceWithDDD.Inventory;
using InvoiceWithDDD.Inventory.GetInventory;
using InvoiceWithDDD.Invoice;
using InvoiceWithDDD.Invoice.UseCases.AddItem;
using InvoiceWithDDD.Invoice.UseCases.CancelInvoice;
using InvoiceWithDDD.Invoice.UseCases.CreateInvoice;
using InvoiceWithDDD.Invoice.UseCases.Finalize;
using InvoiceWithDDD.Invoice.UseCases.GetAllInvoices;
using InvoiceWithDDD.Invoice.UseCases.UpdateItem;
using InvoiceWithDDD.MasterData.Articles;
using InvoiceWithDDD.MasterData.Customers;
using InvoiceWithDDD.MasterData.DBModel;
using InvoiceWithDDD.TaxAdministration;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWithDDD
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

            builder.Services.AddSingleton<CustomerRepository>();

            builder.Services.AddSingleton<InvoiceRepository>();

            builder.Services.AddSingleton<InventoryItemRepository>();

            builder.Services.AddSingleton<InvoiceCommonLogic>();

            builder.Services.AddSingleton<CreateInvoiceCommandHandler>();

            builder.Services.AddSingleton<AddItemCommandHandler>();

            builder.Services.AddSingleton<UpdateItemCommandHandler>();

            builder.Services.AddSingleton<MakeFinalCommandHandler>();

            builder.Services.AddSingleton<CancelInvoiceCommandHandler>();

            builder.Services.AddSingleton<TaxMessageRepository>();
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
