using InvoiceWithLayers.MasterData.Customers;
using InvoiceWithLayers.MasterData.DBModel;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWithLayers.MasterData.Articles
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
                [FromServices] ArticleManager articleManager) =>
                {
                    GetAllArticlesCommand cmd = new();

                    IEnumerable<ArticleDTO> response = articleManager.GetAll(cmd);

                    return response;
                })
                .Produces<IEnumerable<ArticleDTO>>();
        }
    }
}
