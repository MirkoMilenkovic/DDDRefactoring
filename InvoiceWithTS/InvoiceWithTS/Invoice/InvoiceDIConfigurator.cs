using InvoiceWithTS.Inventory;
using InvoiceWithTS.Invoice.UseCases.AddItem;
using InvoiceWithTS.Invoice.UseCases.CancelInvoice;
using InvoiceWithTS.Invoice.UseCases.CreateInvoice;
using InvoiceWithTS.Invoice.UseCases.Finalize;
using InvoiceWithTS.Invoice.UseCases.UpdateItem;

namespace InvoiceWithTS.Invoice
{
    public static class InvoiceDIConfigurator
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<InvoiceRepository>();

            builder.Services.AddSingleton<InvoiceCommonLogic>();

            builder.Services.AddSingleton<CreateInvoiceCommandHandler>();

            builder.Services.AddSingleton<AddItemCommandHandler>();

            builder.Services.AddSingleton<UpdateItemCommandHandler>();

            builder.Services.AddSingleton<MakeFinalCommandHandler>();

            builder.Services.AddSingleton<CancelInvoiceCommandHandler>();

        }
    }
}
