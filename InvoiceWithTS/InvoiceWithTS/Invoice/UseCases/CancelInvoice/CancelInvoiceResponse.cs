using InvoiceWithTS.Invoice.DTO;

namespace InvoiceWithTS.Invoice.UseCases.CancelInvoice
{
    public record CancelInvoiceResponse(
       InvoiceDTO Invoice,
       IEnumerable<InvoiceItemDTO> ItemList);
}
