using InvoiceWithDE.Invoice.DTO;

namespace InvoiceWithDE.Invoice.UseCases.AddItem
{
    public record AddItemResponse(
        InvoiceDTO Invoice, 
        IEnumerable<InvoiceItemDTO> ItemList,
        InvoiceItemDTO AddedItem);
}
