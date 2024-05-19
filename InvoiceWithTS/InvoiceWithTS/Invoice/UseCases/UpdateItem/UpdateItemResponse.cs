using InvoiceWithTS.Invoice.DTO;

namespace InvoiceWithTS.Invoice.UseCases.UpdateItem
{
    public record UpdateItemResponse(
        InvoiceDTO Invoice, 
        IEnumerable<InvoiceItemDTO> ItemList);
}
