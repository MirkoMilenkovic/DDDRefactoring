using InvoiceWithTS.Invoice.BusinessModel;
using InvoiceWithTS.Invoice.DTO;

namespace InvoiceWithTS.Invoice.UseCases.CreateInvoice
{
    public class CreateInvoiceCommandHandler
    {
        private InvoiceRepository _invoiceRepo;

        public CreateInvoiceCommandHandler(InvoiceRepository invoiceRepo)
        {
            _invoiceRepo = invoiceRepo;
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

            _invoiceRepo.Save(
                invoiceModel);

            // Id will be assigned
            return invoiceModel;
        }
    }
}
