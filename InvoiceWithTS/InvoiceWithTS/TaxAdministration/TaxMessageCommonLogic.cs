namespace InvoiceWithTS.TaxAdministration
{
    public class TaxMessageCommonLogic
    {
        public InvoiceWithTS.TaxAdministration.TaxMessageInvoiceStatuses MapInvoiceStatus(
            Invoice.DTO.InvoiceStatuses invoiceStatus)
        {
            switch(invoiceStatus)
            {
                case Invoice.DTO.InvoiceStatuses.Draft:
                    throw new InvalidOperationException($"You can not create TaxMessage for Draft Invoice");
                case Invoice.DTO.InvoiceStatuses.Final:
                    return TaxMessageInvoiceStatuses.MakeTaxmanHappy;
                case Invoice.DTO.InvoiceStatuses.Canceled:
                    return TaxMessageInvoiceStatuses.MakeTaxmanUnhappy;
                default:
                    throw new ArgumentOutOfRangeException($"{invoiceStatus} can not be mapped");
            }
        }
    }
}
