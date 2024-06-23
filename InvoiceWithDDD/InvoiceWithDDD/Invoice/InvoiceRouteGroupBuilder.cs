namespace InvoiceWithDDD.Invoice
{
    public static class InvoiceRouteGroupBuilder
    {
        public static RouteGroupBuilder Get(WebApplication app)
        {
            string route = "/invoice";

            RouteGroupBuilder invoiceGroupBuilder = app
                .MapGroup(route)
                .WithTags("Invoice");

            return invoiceGroupBuilder;
        }
    }
}
