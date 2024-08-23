using InvoiceWithDE.DB;
using InvoiceWithDE.MasterData.Articles;
using InvoiceWithDE.MasterData.DBModel;

namespace InvoiceWithDE.MasterData.Customers
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
