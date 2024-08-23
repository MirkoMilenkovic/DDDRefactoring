using InvoiceWithDE.Inventory;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InvoiceWithDE.Inventory.GetInventory
{
    public static class GetInventoryMinimalApi
    {
        public static void ConfigureGetInventoryMinimalApi(this WebApplication app)
        {
            string inventoryRouteUrl = "/inventory";

            RouteGroupBuilder inventoryRoute = app
                .MapGroup(inventoryRouteUrl)
                .WithTags("Inventory");

            inventoryRoute.MapGet(
                    pattern: "/all",
                    handler: All);
        }

        private static Ok<GetInventoryResponse> All(
            InventoryItemRepository repo)
        {
            IEnumerable<InventoryItemDTO> inventoryItemList = repo.GetAll();

            GetInventoryResponse response = new GetInventoryResponse(
                Items: inventoryItemList);

            return TypedResults.Ok(response);
        }
    }
}
