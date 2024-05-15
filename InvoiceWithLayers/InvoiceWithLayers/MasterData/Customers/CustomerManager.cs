using InvoiceWithLayers.MasterData.DBModel;

namespace InvoiceWithLayers.MasterData.Customers
{
    public class CustomerManager
    {
        private CustomerRepository _repo;

        public CustomerManager(CustomerRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<CustomerDTO> GetAll(GetAllCustomersCommand cmd)
        {
            return _repo.GetAll();
        }
    }
}
