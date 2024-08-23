using InvoiceWithDE.MasterData.Customers;
using InvoiceWithDE.MasterData.DBModel;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWithDE.TaxAdministration
{
    public static class TaxMessageMinimalApi
    {
        public static void ConfigureTaxMessageMinimalApi(this WebApplication app)
        {
            string taxMessageRouteUrl = "/tax-message";

            RouteGroupBuilder taxMessageRoute = app
                .MapGroup(taxMessageRouteUrl)
                .WithTags("TaxMessage");

            taxMessageRoute.MapGet(
                "/all",
                (HttpContext context,
                [FromServices] TaxMessageRepository taxMessageRepo) =>
                {
                    IEnumerable<TaxMessageDTO> response = taxMessageRepo.GetAll();

                    return response;
                })
                .Produces<IEnumerable<TaxMessageDTO>>()
                ;
        }

    }
}
