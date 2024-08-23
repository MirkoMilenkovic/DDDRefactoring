using InvoiceWithDE.Invoice.BusinessModel;
using InvoiceWithDE.Invoice.DTO;

namespace InvoiceWithDE.Invoice.UseCases.CreateInvoice
{
    public class CreateInvoiceCommandHandler
    {
        private readonly InvoiceRepository _invoiceRepo;

        public CreateInvoiceCommandHandler(
            InvoiceRepository invoiceRepo)
        {
            _invoiceRepo = invoiceRepo;
        }


        /// <summary>
        /// Returns new Invoice.
        /// </summary>
        public InvoiceModel Create(CreateInvoiceCommand command)
        {
            // Business logic for new invoice follows...

            // DDD

            InvoiceModel invoiceModel = InvoiceModel.CreateNew(
                invoiceNumber: command.InvoiceNumber,
                customerId: command.CustomerId);

            // Note that business rules below have been moved to factory method or constructor.
            /*
            InvoiceModel invoiceModel = new InvoiceModel()
            {
                EntityState = Common.EntityStates.New, // we are creating New
                // DDD
                // Id = 0, // DB will assign
                // This is not required anymore
                // END DDD
                CustomerId = command.CustomerId, // by user
                InvoiceNumber = command.InvoiceNumber, // by user
                InvoiceDate = DateTime.Today, // business rule
                Status = InvoiceStatuses.Draft, // business rule
                PriceWithoutTax = 0, // business rule
                PriceWithTax = 0, // business rule
                TaxAtNormalRate = 0, // business rule
                TaxAtReducedRate = 0, // business rule
            };
            */
            // END DDD

            _invoiceRepo.Save(
                invoiceModel);

            // Id will be assigned
            return invoiceModel;
        }
    }
}
