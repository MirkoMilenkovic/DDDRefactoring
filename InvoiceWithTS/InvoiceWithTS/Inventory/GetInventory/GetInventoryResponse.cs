using InvoiceWithTS.Inventory;

namespace InvoiceWithTS.Inventory.GetInventory
{
    public record GetInventoryResponse(IEnumerable<InventoryItemDTO> Items);
}
