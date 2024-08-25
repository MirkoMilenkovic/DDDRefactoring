
using InvoiceWithDDD.Invoice.UseCases.AddItem;
using InvoiceWithDDD.Invoice.UseCases.CancelInvoice;
using InvoiceWithDDD.Invoice.UseCases.CreateInvoice;
using InvoiceWithDDD.Invoice.UseCases.Finalize;
using InvoiceWithDDD.Invoice.UseCases.UpdateItem;

namespace InvoiceWithDDD.Invoice
{
    public static class InvoiceDIConfigurator
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<InvoiceRepository>();

            // DDD
            // No InvoiceCommonLogic,
            // because it is moved to InvoiceModel itself. 
            // builder.Services.AddSingleton<InvoiceCommonLogic>();
            // END DDD

            builder.Services.AddSingleton<CreateInvoiceCommandHandler>();

            builder.Services.AddSingleton<AddItemCommandHandler>();

            builder.Services.AddSingleton<UpdateItemCommandHandler>();

            builder.Services.AddSingleton<MakeFinalCommandHandler>();

            builder.Services.AddSingleton<CancelInvoiceCommandHandler>();
        }
    }
}
