using InvoiceWithTS.DB;
using InvoiceWithTS.Inventory;
using InvoiceWithTS.Inventory.GetInventory;
using InvoiceWithTS.Invoice;
using InvoiceWithTS.Invoice.UseCases.AddItem;
using InvoiceWithTS.Invoice.UseCases.CreateInvoice;
using InvoiceWithTS.Invoice.UseCases.Finalize;
using InvoiceWithTS.Invoice.UseCases.GetAllInvoices;
using InvoiceWithTS.Invoice.UseCases.UpdateItem;
using InvoiceWithTS.MasterData.Articles;
using InvoiceWithTS.MasterData.Customers;
using InvoiceWithTS.MasterData.DBModel;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWithTS
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

            builder.Services.AddSingleton<CustomerRepository>();

            builder.Services.AddSingleton<InvoiceRepository>();

            builder.Services.AddSingleton<InventoryItemRepository>();

            builder.Services.AddSingleton<InvoiceCommonLogic>();

            builder.Services.AddSingleton<CreateInvoiceCommandHandler>();

            builder.Services.AddSingleton<AddItemCommandHandler>();

            builder.Services.AddSingleton<UpdateItemCommandHandler>();

            builder.Services.AddSingleton<MakeFinalCommandHandler>();


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
        }

    }
}
