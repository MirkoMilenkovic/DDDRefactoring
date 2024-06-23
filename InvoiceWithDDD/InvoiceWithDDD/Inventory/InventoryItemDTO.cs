using InvoiceWithDDD.Common;

namespace InvoiceWithDDD.Inventory
{
    public class InventoryItemDTO : BaseDTO
    {
        public required int ArticleId { get; set; }

        public required int Quantity { get; set; }
    }
}
