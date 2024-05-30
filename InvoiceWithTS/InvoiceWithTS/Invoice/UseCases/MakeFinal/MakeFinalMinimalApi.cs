using InvoiceWithTS.Invoice.BusinessModel;
using InvoiceWithTS.Invoice.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWithTS.Invoice.UseCases.Finalize
{
    public static class MakeFinalMinimalApi
    {
        public static void ConfigureMakeFinalMinimalApi(this WebApplication app)
        {
            RouteGroupBuilder invoiceGroupBuilder = InvoiceRouteGroupBuilder.Get(app);

            invoiceGroupBuilder.MapPatch(
                    pattern: "/make-final",
                    handler: MakeFinal);
        }

        private static Ok<MakeFinalResponse> MakeFinal(
           [FromBody] MakeFinalCommand finalizeRequest,
           MakeFinalCommandHandler commandHandler)
        {
            InvoiceModel invoiceModel = commandHandler.MakeFinal(request: finalizeRequest);

            InvoiceDTO invoiceDTO = InvoiceModel.ToDTO(invoiceModel);

            List<InvoiceItemDTO> itemDTOList = new();
            foreach (var itemModel in invoiceModel.Items)
            {
                InvoiceItemDTO itemDTO = InvoiceItemModel.ToDTO(itemModel);
                itemDTOList.Add(itemDTO);
            }

            MakeFinalResponse response = new MakeFinalResponse(
                invoiceDTO,
                itemDTOList);

            return TypedResults.Ok(response);
        }
    }
}
