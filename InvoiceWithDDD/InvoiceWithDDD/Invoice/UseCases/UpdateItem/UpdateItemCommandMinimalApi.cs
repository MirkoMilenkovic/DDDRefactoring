using InvoiceWithDDD.Invoice.BusinessModel;
using InvoiceWithDDD.Invoice.DTO;
using InvoiceWithDDD.Invoice.UseCases.CreateInvoice;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWithDDD.Invoice.UseCases.UpdateItem
{
    public static class UpdateItemCommandMinimalApi
    {
        public static void ConfigureUpdateItemMinimalApi(this WebApplication app)
        {
            RouteGroupBuilder invoiceGroupBuilder = InvoiceRouteGroupBuilder.Get(app);

            invoiceGroupBuilder.MapPatch(
                    pattern: "/update-item",
                    handler: UpdateItem);
        }

        private static Ok<UpdateItemResponse> UpdateItem(
            [FromBody] UpdateItemCommand addItemRequest,
            UpdateItemCommandHandler commandHandler)
        {
            (InvoiceModel Invoice, InvoiceItemModel UpdatedItem) updateResult = commandHandler.UpdateItem(addItemRequest);

            InvoiceDTO invoiceDTO = InvoiceModel.ToDTO(updateResult.Invoice);

            List<InvoiceItemDTO> itemDTOList = new();
            foreach(var itemModel in updateResult.Invoice.Items)
            {
                InvoiceItemDTO itemDTO = InvoiceItemModel.ToDTO(itemModel);
                itemDTOList.Add(itemDTO);
            }

            InvoiceItemDTO updatedItemDTO = InvoiceItemModel.ToDTO(updateResult.UpdatedItem);
            
            UpdateItemResponse response = new UpdateItemResponse(
                invoiceDTO,
                itemDTOList,
                updatedItemDTO);

            return TypedResults.Ok(response);
        }
    }
}
