using InvoiceWithLayers.MasterData.Customers;
using InvoiceWithLayers.MasterData.DBModel;

namespace InvoiceWithLayers.MasterData.Articles
{
    public class ArticleManager
    {
        private ArticleRepository _repo;

        public ArticleManager(ArticleRepository articleRepository)
        {
            _repo = articleRepository;
        }

        public IEnumerable<ArticleDTO> GetAll(GetAllArticlesCommand cmd)
        {
            return _repo.GetAll();
        }
    }
}
