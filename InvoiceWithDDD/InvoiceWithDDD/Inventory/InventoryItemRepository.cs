using InvoiceWithDDD.DB;
using InvoiceWithDDD.MasterData.Articles;

namespace InvoiceWithDDD.Inventory
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

        public void ReduceQuantity(
            int articleId,
            int quantity)
        {
            InventoryItemDTO inventoryItem = GetByArticleId(articleId);

            inventoryItem.Quantity -= quantity;
        }


        public void IncreaseQuantity(
            int articleId,
            int quantity)
        {
            InventoryItemDTO inventoryItem = GetByArticleId(articleId);

            inventoryItem.Quantity += quantity;
        }


        private InventoryItemDTO GetByArticleId(
            int articleId)
        {
            InventoryItemDTO? inventoryItem = _db.InventoryItem.Values.FirstOrDefault(
                x => x.ArticleId == articleId);

            if (inventoryItem == null)
            {
                // something very bad
                throw new Exception($"inventoryItem with ArticleId {articleId} does not exist in DB");
            }

            return inventoryItem;
        }
    }
}
