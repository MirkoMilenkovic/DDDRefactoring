using InvoiceWithDDD.Invoice.DTO;

namespace InvoiceWithDDD.Invoice.UseCases.AddItem
{
    public record AddItemResponse(
        InvoiceDTO Invoice, 
        IEnumerable<InvoiceItemDTO> ItemList,
        InvoiceItemDTO AddedItem);
}
