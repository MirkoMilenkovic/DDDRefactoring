using InvoiceWithDE.Invoice.DTO;

namespace InvoiceWithDE.Invoice.UseCases.CancelInvoice
{
    public record CancelInvoiceResponse(
       InvoiceDTO Invoice,
       IEnumerable<InvoiceItemDTO> ItemList);
}
