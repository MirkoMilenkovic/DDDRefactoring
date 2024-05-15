using InvoiceWithTS.MasterData.DBModel;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWithTS.MasterData.Customers
{
    public static class CustomerMinimalApi
    {

        public static void ConfigureCustomerMinimalApi(this WebApplication app)
        {
            string customerRouteUrl = "/customer";

            RouteGroupBuilder customerRoute = app.MapGroup(customerRouteUrl).WithTags("Customer");

            customerRoute.MapGet(
                "/all",
                (HttpContext context,
                //[AsParameters] GetAllCustomersRequest request,
                [FromServices] CustomerManager customerManager) =>
                {
                    GetAllCustomersCommand cmd = new();

                    IEnumerable<CustomerDTO> response = customerManager.GetAll(cmd);

                    return response;
                })
                .Produces<IEnumerable<CustomerDTO>>()
                ;
        }
    }
}
