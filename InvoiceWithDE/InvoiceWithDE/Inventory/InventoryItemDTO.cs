using InvoiceWithDE.Common;

namespace InvoiceWithDE.Inventory
{
    public class InventoryItemDTO : BaseDTO
    {
        public required int ArticleId { get; set; }

        public required int Quantity { get; set; }
    }
}
