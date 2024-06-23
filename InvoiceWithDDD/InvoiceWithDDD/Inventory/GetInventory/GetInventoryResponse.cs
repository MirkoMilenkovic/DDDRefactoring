using InvoiceWithDDD.Inventory;

namespace InvoiceWithDDD.Inventory.GetInventory
{
    public record GetInventoryResponse(IEnumerable<InventoryItemDTO> Items);
}
