using InvoiceWithDDD.Invoice.BusinessModel;
using InvoiceWithDDD.Invoice.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceWithDDD.Invoice.UseCases.CreateInvoice
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
            CreateInvoiceCommandHandler commandHandler)
        {
            InvoiceModel invoiceModel = commandHandler.Create(createRequest);

            InvoiceDTO invoiceDTO = InvoiceModel.ToDTO(invoiceModel);
            CreateInvoiceResponse response = new CreateInvoiceResponse(invoiceDTO);
            return TypedResults.Ok(response);
        }
    }
}
