using InvoiceWithTS.Invoice.BusinessModel;
using InvoiceWithTS.Invoice.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWithTS.Invoice.UseCases.CreateInvoice
{
    public static class CreateInvoiceMinimalApi
    {
        public static void ConfigureCreateInvoiceMinimalApi(this WebApplication app)
        {
            RouteGroupBuilder invoiceGroupBuilder = InvoiceRouteGroupBuilder.Get(app);

            invoiceGroupBuilder.MapPost(
                    pattern: "/create",
                    handler: Create);
        }

        /// <summary>
        /// Creates new.
        /// </summary>
        /// <returns>
        /// Returns new InvoiceDTO.
        /// </returns>
        // Yeah, yeah, I know that C in CQS shoud not return anything. If so, how is user going to see business rules that were applied?
        private static Ok<CreateInvoiceResponse> Create(
            [FromBody] CreateInvoiceCommand createRequest,
            InvoiceCommandHandler invoiceManager)
        {
            InvoiceModel invoiceModel = invoiceManager.Create(createRequest);

            InvoiceDTO invoiceDTO = InvoiceModel.ToDTO(invoiceModel);
            CreateInvoiceResponse response = new CreateInvoiceResponse(invoiceDTO);
            return TypedResults.Ok(response);
        }
    }
}
