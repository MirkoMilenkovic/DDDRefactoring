using InvoiceWithDDD.MasterData.Customers;
using InvoiceWithDDD.MasterData.DBModel;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWithDDD.MasterData.Articles
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
