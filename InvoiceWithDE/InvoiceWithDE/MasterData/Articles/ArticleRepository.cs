using InvoiceWithDE.DB;

namespace InvoiceWithDE.MasterData.Articles
{
    public class ArticleRepository
    {
        private InMemoryDB _db;

        public ArticleRepository(InMemoryDB db)
        {
            _db = db;
        }

        public IEnumerable<ArticleDTO> GetAll()
        {
            return _db.Article.Values;
        }

        public ArticleDTO? GetById(int id)
        {
            _db.Article.TryGetValue(
                id,
                out var article) ;

            return article;
        }
    }
}
