using InvoiceWithTS.Invoice.DTO;

namespace InvoiceWithTS.Invoice.UseCases.AddItem
{
    public record AddItemResponse(
        InvoiceDTO Invoice, 
        IEnumerable<InvoiceItemDTO> ItemList);
}
