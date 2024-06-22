﻿using InvoiceWithTS.MasterData.Customers;
using InvoiceWithTS.MasterData.DBModel;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWithTS.TaxAdministration
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
