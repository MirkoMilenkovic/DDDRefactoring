using InvoiceWithDE.Invoice.DTO;

namespace InvoiceWithDE.Invoice.UseCases.GetAllInvoices
{
    public record GetAllInvoicesResponseItem(
        InvoiceDTO Invoice,
        IEnumerable<InvoiceItemDTO> Items);

}
