using InvoiceWithDDD.DB;
using InvoiceWithDDD.MasterData.Articles;
using InvoiceWithDDD.MasterData.DBModel;

namespace InvoiceWithDDD.MasterData.Customers
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
