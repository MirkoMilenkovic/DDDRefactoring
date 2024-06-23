using InvoiceWithDDD.DB;

namespace InvoiceWithDDD.TaxAdministration
{
    public class TaxMessageRepository
    {
        private readonly InMemoryDB _db;

        public TaxMessageRepository(
            InMemoryDB db)
        {
            _db = db;
        }

        public void EnqueueForSending(
            TaxMessageDTO taxMessageDTO)
        {
            taxMessageDTO.Id = _db.GetNextId();

            _db.TaxMessage.Add(
                taxMessageDTO.Id,
                taxMessageDTO);
        }

        public IEnumerable<TaxMessageDTO> GetAll()
        {
            return _db.TaxMessage.Values;
        }
    }
}

