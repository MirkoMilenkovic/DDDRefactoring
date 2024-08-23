using InvoiceWithDE.Invoice.DTO;

namespace InvoiceWithDE.Invoice.UseCases.Finalize
{
    public record MakeFinalResponse(
        InvoiceDTO Invoice,
        IEnumerable<InvoiceItemDTO> ItemList);
}
