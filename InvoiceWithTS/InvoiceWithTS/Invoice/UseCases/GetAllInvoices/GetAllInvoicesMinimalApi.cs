using Microsoft.AspNetCore.Http.HttpResults;

namespace InvoiceWithTS.Invoice.UseCases.GetAllInvoices
{
    public static class GetAllInvoicesMinimalApi
    {
        public static void ConfigureGetAllInvoicesMinimalApi(this WebApplication app)
        {
            RouteGroupBuilder invoiceGroupBuilder = InvoiceRouteGroupBuilder.Get(app);

            invoiceGroupBuilder.MapGet(
                    pattern: "/all",
                    handler: All);
        }

        private static Ok<GetAllInvoicesResponse> All(
            InvoiceRepository repo)
        {
            IEnumerable<GetAllInvoicesResponseItem> invoiceList = repo.GetAll();

            GetAllInvoicesResponse response = new GetAllInvoicesResponse(
                InvoiceList: invoiceList);

            return TypedResults.Ok(response);
        }
    }
}
