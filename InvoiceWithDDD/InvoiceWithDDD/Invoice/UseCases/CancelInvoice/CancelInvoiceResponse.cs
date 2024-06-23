using InvoiceWithDDD.Invoice.DTO;

namespace InvoiceWithDDD.Invoice.UseCases.CancelInvoice
{
    public record CancelInvoiceResponse(
       InvoiceDTO Invoice,
       IEnumerable<InvoiceItemDTO> ItemList);
}
