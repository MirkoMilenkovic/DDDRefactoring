using InvoiceWithTS.Common;

namespace InvoiceWithTS.MasterData.Inventory
{
    public class InventoryItemDTO : BaseDTO
    {
        public required int ArticleId { get; set; }

        public required int Quantity { get; set; }
    }
}
