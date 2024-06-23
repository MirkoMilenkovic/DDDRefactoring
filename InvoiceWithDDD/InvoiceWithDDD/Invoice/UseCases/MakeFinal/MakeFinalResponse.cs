using InvoiceWithDDD.Invoice.DTO;

namespace InvoiceWithDDD.Invoice.UseCases.Finalize
{
    public record MakeFinalResponse(
        InvoiceDTO Invoice,
        IEnumerable<InvoiceItemDTO> ItemList);
}
