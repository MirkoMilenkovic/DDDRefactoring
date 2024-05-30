using InvoiceWithTS.Invoice.BusinessModel;
using System.Transactions;

namespace InvoiceWithTS.Invoice.UseCases.Finalize
{
    public class MakeFinalCommandHandler
    {
        private InvoiceCommonLogic _commonLogic;

        private InvoiceRepository _invoiceRepo;

        public MakeFinalCommandHandler(
            InvoiceCommonLogic commonLogic, 
            InvoiceRepository invoiceRepo)
        {
            _commonLogic = commonLogic;
            _invoiceRepo = invoiceRepo;
        }

        public InvoiceModel MakeFinal(MakeFinalCommand request)
        {
            InvoiceModel? invoiceModel = _invoiceRepo.GetById(
               request.InvoiceId);

            if (invoiceModel == null)
            {
                throw new Exception($"{request.InvoiceId} not found");
            }

            // apply change            
            invoiceModel.Status = DTO.InvoiceStatuses.Final;

            // start save
            using TransactionScope ts = new TransactionScope();

            // Save invoice
            _invoiceRepo.Save(
                invoiceModel);

            // complete tran
            ts.Complete();

            return invoiceModel;
        }
    }
}
