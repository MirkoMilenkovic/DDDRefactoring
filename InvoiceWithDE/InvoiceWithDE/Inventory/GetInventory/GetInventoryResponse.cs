using InvoiceWithDE.Inventory;

namespace InvoiceWithDE.Inventory.GetInventory
{
    public record GetInventoryResponse(IEnumerable<InventoryItemDTO> Items);
}
