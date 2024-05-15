namespace InvoiceWithTS.Invoice.BusinessModel
{
    public class InvoiceItemModel
    {
        public required int Id { get; set; }

        public required InvoiceModel Invoice { get; set; }

        public required int ArticleId { get; set; }

        public required int Quantity { get; set; }

        public required decimal PriceWithoutTax { get; set; }

        public required decimal Tax { get; set; }

        public required decimal PriceWithTax { get; set; }
    }
}
