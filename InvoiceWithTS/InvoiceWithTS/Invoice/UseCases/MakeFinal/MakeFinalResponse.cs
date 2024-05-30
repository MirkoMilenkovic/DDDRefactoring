using InvoiceWithTS.Invoice.DTO;

namespace InvoiceWithTS.Invoice.UseCases.Finalize
{
    public record MakeFinalResponse(
        InvoiceDTO Invoice,
        IEnumerable<InvoiceItemDTO> ItemList);
}
