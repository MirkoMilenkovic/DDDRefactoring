namespace InvoiceWithDDD.Invoice.UseCases.AddItem
{
    public record AddItemCommand(
        int InvoiceId, 
        int ArticleId,
        int Quantity
        );
}
