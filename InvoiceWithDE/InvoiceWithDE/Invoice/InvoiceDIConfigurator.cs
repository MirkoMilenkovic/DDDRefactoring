
using InvoiceWithDE.Invoice.UseCases.AddItem;
using InvoiceWithDE.Invoice.UseCases.CancelInvoice;
using InvoiceWithDE.Invoice.UseCases.CreateInvoice;
using InvoiceWithDE.Invoice.UseCases.Finalize;
using InvoiceWithDE.Invoice.UseCases.UpdateItem;

namespace InvoiceWithDE.Invoice
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
