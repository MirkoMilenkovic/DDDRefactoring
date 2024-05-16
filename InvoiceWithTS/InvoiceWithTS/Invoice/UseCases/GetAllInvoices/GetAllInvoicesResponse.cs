using InvoiceWithTS.Invoice.DTO;

namespace InvoiceWithTS.Invoice.UseCases.GetAllInvoices
{
    public record GetAllInvoicesResponse(IEnumerable<GetAllInvoicesResponseItem> InvoiceList);

}
