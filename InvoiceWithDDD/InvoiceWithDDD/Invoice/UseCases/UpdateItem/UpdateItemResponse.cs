using InvoiceWithDDD.Invoice.DTO;

namespace InvoiceWithDDD.Invoice.UseCases.UpdateItem
{
    public record UpdateItemResponse(
        InvoiceDTO Invoice,
        IEnumerable<InvoiceItemDTO> ItemList,
        InvoiceItemDTO UpdatedItem);
}
