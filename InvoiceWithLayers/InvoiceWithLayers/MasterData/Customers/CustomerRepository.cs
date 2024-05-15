using InvoiceWithLayers.DB;
using InvoiceWithLayers.MasterData.Articles;
using InvoiceWithLayers.MasterData.DBModel;

namespace InvoiceWithLayers.MasterData.Customers
{
    public class CustomerRepository
    {
        private InMemoryDB _db;

        public CustomerRepository(InMemoryDB db)
        {
            _db = db;
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            return _db.Customer.Values;
        }
    }
}
