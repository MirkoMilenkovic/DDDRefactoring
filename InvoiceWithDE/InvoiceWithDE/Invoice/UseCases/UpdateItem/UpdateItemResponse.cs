using InvoiceWithDE.Invoice.DTO;

namespace InvoiceWithDE.Invoice.UseCases.UpdateItem
{
    public record UpdateItemResponse(
        InvoiceDTO Invoice,
        IEnumerable<InvoiceItemDTO> ItemList,
        InvoiceItemDTO UpdatedItem);
}
