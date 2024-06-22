using InvoiceWithTS.Invoice.BusinessModel;
using InvoiceWithTS.Invoice.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWithTS.Invoice.UseCases.AddItem
{
    public static class UpdateItemCommandMinimalApi
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
            (InvoiceModel Invoice, InvoiceItemModel AddedItem) addResult = commandHandler.AddItem(addItemRequest);

            InvoiceDTO invoiceDTO = InvoiceModel.ToDTO(addResult.Invoice);

            List<InvoiceItemDTO> itemDTOList = new();
            foreach (InvoiceItemModel itemModel in addResult.Invoice.Items)
            {
                InvoiceItemDTO itemDTO = InvoiceItemModel.ToDTO(itemModel);
                itemDTOList.Add(itemDTO);
            }

            InvoiceItemDTO addedItemDTO = InvoiceItemModel.ToDTO(addResult.AddedItem);
            
            AddItemResponse response = new AddItemResponse(
                invoiceDTO,
                itemDTOList,
                addedItemDTO);

            return TypedResults.Ok(response);
        }
    }
}
