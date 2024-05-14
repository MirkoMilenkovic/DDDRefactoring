using InvoiceWithLayers.DB;

namespace InvoiceWithLayers.MasterData.Articles
{
    public class ArticleRepository
    {
        private InMemoryDB _db;

        public ArticleRepository(InMemoryDB db)
        {
            _db = db;
        }

        public IEnumerable<ArticleDBT> GetAll()
        {
            return _db.Article.Values;
        }
    }
}
