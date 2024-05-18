using InvoiceWithTS.Invoice.BusinessModel;
using InvoiceWithTS.Invoice.DTO;
using InvoiceWithTS.Invoice.UseCases.CreateInvoice;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWithTS.Invoice.UseCases.AddItem
{
    public static class AddItemCommandMinimalApi
    {
        public static void ConfigureAddItemMinimalApi(this WebApplication app)
        {
            RouteGroupBuilder invoiceGroupBuilder = InvoiceRouteGroupBuilder.Get(app);

            invoiceGroupBuilder.MapPost(
                    pattern: "/add-item",
                    handler: AddItem);
        }

        private static Ok<AddItemResponse> AddItem(
            [FromBody] AddItemCommand addItemRequest,
            AddItemCommandHandler commandHandler)
        {
            InvoiceModel invoiceModel = commandHandler.AddItem(addItemRequest);

            InvoiceDTO invoiceDTO = InvoiceModel.ToDTO(invoiceModel);

            List<InvoiceItemDTO> itemDTOList = new();
            foreach(var itemModel in invoiceModel.Items)
            {
                InvoiceItemDTO itemDTO = InvoiceItemModel.ToDTO(itemModel);
                itemDTOList.Add(itemDTO);
            }
            
            AddItemResponse response = new AddItemResponse(
                invoiceDTO,
                itemDTOList);

            return TypedResults.Ok(response);
        }
    }
}
