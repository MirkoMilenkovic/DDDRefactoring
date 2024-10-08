﻿using InvoiceWithDE.MasterData.Customers;
using InvoiceWithDE.MasterData.DBModel;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWithDE.MasterData.Articles
{
    public static class ArticleMinimalApi
    {
        public static void ConfigureArticleMinimalApi(this WebApplication app)
        {
            string articleRouteUrl = "/article";

            RouteGroupBuilder articleRoute = app
                .MapGroup(articleRouteUrl)
                .WithTags("Article");

            articleRoute.MapGet(
                "/all",
                (HttpContext context,
                //[AsParameters] GetAllCustomersRequest request,
                [FromServices] ArticleRepository articleRepo) =>
                {
                    IEnumerable<ArticleDTO> response = articleRepo.GetAll();

                    return response;
                })
                .Produces<IEnumerable<ArticleDTO>>();
        }
    }
}
