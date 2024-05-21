using InvoiceWithTS.Invoice.BusinessModel;
using InvoiceWithTS.Invoice.DTO;
using InvoiceWithTS.Invoice.UseCases.CreateInvoice;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWithTS.Invoice.UseCases.UpdateItem
{
    public static class UpdateItemCommandMinimalApi
    {
        public static void ConfigureUpdateItemMinimalApi(this WebApplication app)
        {
            RouteGroupBuilder invoiceGroupBuilder = InvoiceRouteGroupBuilder.Get(app);

            invoiceGroupBuilder.MapPost(
                    pattern: "/update-item",
                    handler: UpdateItem);
        }

        private static Ok<UpdateItemResponse> UpdateItem(
            [FromBody] UpdateItemCommand addItemRequest,
            UpdateItemCommandHandler commandHandler)
        {
            InvoiceModel invoiceModel = commandHandler.UpdateItem(addItemRequest);

            InvoiceDTO invoiceDTO = InvoiceModel.ToDTO(invoiceModel);

            List<InvoiceItemDTO> itemDTOList = new();
            foreach(var itemModel in invoiceModel.Items)
            {
                InvoiceItemDTO itemDTO = InvoiceItemModel.ToDTO(itemModel);
                itemDTOList.Add(itemDTO);
            }
            
            UpdateItemResponse response = new UpdateItemResponse(
                invoiceDTO,
                itemDTOList);

            return TypedResults.Ok(response);
        }
    }
}
