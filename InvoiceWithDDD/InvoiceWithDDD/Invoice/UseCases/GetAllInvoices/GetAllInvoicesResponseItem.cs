using InvoiceWithDDD.Invoice.DTO;

namespace InvoiceWithDDD.Invoice.UseCases.GetAllInvoices
{
    public record GetAllInvoicesResponseItem(
        InvoiceDTO Invoice,
        IEnumerable<InvoiceItemDTO> Items);

}
