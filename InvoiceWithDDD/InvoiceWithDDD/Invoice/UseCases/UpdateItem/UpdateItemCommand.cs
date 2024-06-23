namespace InvoiceWithDDD.Invoice.UseCases.UpdateItem
{
    public record UpdateItemCommand(
        int InvoiceId,
        int ItemId,
        int Quantity
        );
}
