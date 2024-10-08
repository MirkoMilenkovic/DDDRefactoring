﻿using InvoiceWithDE.MasterData.Articles;
using InvoiceWithDE.MasterData.DBModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWithDE.MasterData.Customers
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
                [FromServices] CustomerRepository customerRepo) =>
                {
                    IEnumerable<CustomerDTO> response = customerRepo.GetAll();

                    return response;
                })
                .Produces<IEnumerable<CustomerDTO>>()
                ;
        }
    }
}
