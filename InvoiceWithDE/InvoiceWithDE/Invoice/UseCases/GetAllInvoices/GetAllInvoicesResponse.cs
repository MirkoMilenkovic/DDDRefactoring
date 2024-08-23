using InvoiceWithDE.Invoice.DTO;

namespace InvoiceWithDE.Invoice.UseCases.GetAllInvoices
{
    public record GetAllInvoicesResponse(IEnumerable<GetAllInvoicesResponseItem> InvoiceList);

}
