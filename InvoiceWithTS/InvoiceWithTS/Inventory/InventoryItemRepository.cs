using InvoiceWithTS.DB;
using InvoiceWithTS.MasterData.Articles;

namespace InvoiceWithTS.Inventory
{
    public class InventoryItemRepository
    {
        private InMemoryDB _db;

        public InventoryItemRepository(InMemoryDB db)
        {
            _db = db;
        }

        public IEnumerable<InventoryItemDTO> GetAll()
        {
            return _db.InventoryItem.Values;
        }
    }
}
