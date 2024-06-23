namespace InvoiceWithDDD.Invoice.UseCases.CreateInvoice
{
    public record CreateInvoiceCommand(
        int CustomerId,
        string InvoiceNumber)
        ;
}
