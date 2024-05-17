using InvoiceWithTS.Invoice.BusinessModel;
using InvoiceWithTS.Invoice.DTO;
using InvoiceWithTS.Invoice.UseCases.CreateInvoice;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWithTS.Invoice.UseCases.AddItem
{
    public static class AddItemCommandMinimalApi
    {
        public static void ConfigureAddItemCommandMinimalApi(this WebApplication app)
        {
            RouteGroupBuilder invoiceGroupBuilder = InvoiceRouteGroupBuilder.Get(app);

            invoiceGroupBuilder.MapPost(
                    pattern: "/add-item",
                    handler: AddItem);
        }

        private static Ok<AddItemResponse> AddItem(
            [FromBody] AddItemCommand addItemRequest,
            InvoiceCommandHandler commandHandler)
        {
            InvoiceModel invoiceModel = commandHandler.AddItem(addItemRequest);

            InvoiceDTO invoiceDTO = InvoiceModel.ToDTO(invoiceModel);
            AddItemResponse response = new AddItemResponse(invoiceDTO);
            return TypedResults.Ok(response);
        }
    }
}
