using InvoiceWithDDD.Invoice.BusinessModel;
using InvoiceWithDDD.Invoice.DTO;
using InvoiceWithDDD.Invoice.UseCases.Finalize;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWithDDD.Invoice.UseCases.CancelInvoice
{
    public static class CancelInvoiceMinimalApi
    {
        public static void ConfigureCancelInvoiceMinimalApi(this WebApplication app)
        {
            RouteGroupBuilder invoiceGroupBuilder = InvoiceRouteGroupBuilder.Get(app);

            invoiceGroupBuilder.MapPatch(
                    pattern: "/cancel",
                    handler: Cancel);
        }

        private static Ok<CancelInvoiceResponse> Cancel(
           [FromBody] CancelInvoiceCommand cancelRequest,
           CancelInvoiceCommandHandler commandHandler)
        {
            InvoiceModel invoiceModel = commandHandler.Cancel(request: cancelRequest);

            InvoiceDTO invoiceDTO = InvoiceModel.ToDTO(invoiceModel);

            List<InvoiceItemDTO> itemDTOList = new();
            foreach (var itemModel in invoiceModel.Items)
            {
                InvoiceItemDTO itemDTO = InvoiceItemModel.ToDTO(itemModel);
                itemDTOList.Add(itemDTO);
            }

            CancelInvoiceResponse response = new CancelInvoiceResponse(
                invoiceDTO,
                itemDTOList);

            return TypedResults.Ok(response);
        }
    }
}
