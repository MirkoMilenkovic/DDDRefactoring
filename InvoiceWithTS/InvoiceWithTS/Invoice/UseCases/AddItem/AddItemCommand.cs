namespace InvoiceWithTS.Invoice.UseCases.AddItem
{
    public record UpdateItemCommand(
        int InvoiceId, 
        int ArticleId,
        int Quantity
        );
}
