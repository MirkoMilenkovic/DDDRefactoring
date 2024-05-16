using InvoiceWithTS.Invoice.BusinessModel;
using InvoiceWithTS.Invoice.DTO;
using InvoiceWithTS.Invoice.UseCases.CreateInvoice;

namespace InvoiceWithTS.Invoice
{
    public class InvoiceManager
    {
        private InvoiceRepository _repo;
        
        public InvoiceManager(InvoiceRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Returns new Invoice.
        /// </summary>
        public InvoiceModel Create(CreateInvoiceCommand command)
        {
            // Business logic for new invoice follows...

            InvoiceModel invoiceModel = new InvoiceModel()
            {
                EntityState = Common.EntityStates.New, // we are creating New
                Id = 0, // DB will assign
                CustomerId = command.CustomerId, // by user
                InvoiceNumber = command.InvoiceNumber, // by user
                InvoiceDate = DateTime.Today, // business rule
                Status = InvoiceStatuses.Draft, // business rule
                PriceWithoutTax = 0, // business rule
                PriceWithTax = 0, // business rule
                TaxAtNormalRate = 0, // business rule
                TaxAtReducedRate = 0, // business rule
            };

            _repo.Save(
                invoiceModel);

            // Id will be assigned
            return invoiceModel;
        }
    }
}
