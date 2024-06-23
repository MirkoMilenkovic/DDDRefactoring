using InvoiceWithDDD.Invoice.DTO;

namespace InvoiceWithDDD.Invoice.UseCases.GetAllInvoices
{
    public record GetAllInvoicesResponse(IEnumerable<GetAllInvoicesResponseItem> InvoiceList);

}
