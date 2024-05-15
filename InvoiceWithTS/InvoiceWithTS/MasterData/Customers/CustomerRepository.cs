using InvoiceWithTS.DB;
using InvoiceWithTS.MasterData.Articles;
using InvoiceWithTS.MasterData.DBModel;

namespace InvoiceWithTS.MasterData.Customers
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
