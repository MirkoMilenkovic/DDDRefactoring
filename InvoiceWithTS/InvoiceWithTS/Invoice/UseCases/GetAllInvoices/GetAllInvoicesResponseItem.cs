using InvoiceWithTS.Invoice.DTO;

namespace InvoiceWithTS.Invoice.UseCases.GetAllInvoices
{
    public record GetAllInvoicesResponseItem(
        InvoiceDTO Invoice,
        IEnumerable<InvoiceItemDTO> Items);

}
