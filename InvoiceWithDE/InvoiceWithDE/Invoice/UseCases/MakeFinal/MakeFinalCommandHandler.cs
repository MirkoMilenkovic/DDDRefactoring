using InvoiceWithDE.EventIInfrastructure;
using InvoiceWithDE.Inventory;
using InvoiceWithDE.Invoice.BusinessModel;
using InvoiceWithDE.Invoice.DomainEvents;
using InvoiceWithDE.TaxAdministration;
using System.Transactions;

namespace InvoiceWithDE.Invoice.UseCases.Finalize
{
    public class MakeFinalCommandHandler
    {
        private readonly InvoiceRepository _invoiceRepo;

        // DE
        // We do not need to deal with inventory and tax message any more
        /*
        private readonly InventoryItemRepository _inventoryItemRepo;

        private readonly TaxMessageRepository _taxMessageRepository;

        private readonly TaxMessageCommonLogic _taxMessageCommonLogic;
        */
        
        // but we need EventBus
        private readonly EventBus _eventBus;

        // END DE

        public MakeFinalCommandHandler(
            InvoiceRepository invoiceRepo,
            //InventoryItemRepository inventoryItemRepo,
            //TaxMessageRepository taxMessageRepository,
            //TaxMessageCommonLogic taxMessageCommonLogic,
            EventBus eventBus)
        {
            _invoiceRepo = invoiceRepo;
            //_inventoryItemRepo = inventoryItemRepo;
            //_taxMessageRepository = taxMessageRepository;
            //_taxMessageCommonLogic = taxMessageCommonLogic;
            _eventBus = eventBus;
        }

        public InvoiceModel MakeFinal(MakeFinalCommand request)
        {
            InvoiceModel? invoiceModel = _invoiceRepo.GetById(
               request.InvoiceId);

            if (invoiceModel == null)
            {
                throw new Exception($"{request.InvoiceId} not found");
            }

            // DDD

            invoiceModel.MakeFinal();

            // apply change        
            /*
            invoiceModel.Status = DTO.InvoiceStatuses.Final;

            // Do not forget this.
            invoiceModel.EntityState = Common.EntityStates.Updated;
            */
            // END DDD

            // start save
            using TransactionScope ts = new TransactionScope();

            // Save invoice
            _invoiceRepo.Save(
                invoiceModel);

            // DDD
            // The code below remains a problem,
            // because I have to know details of Tax system and Inventory.
            // We will try to solve it for next release.    

            //reduce inventory

            // DE
            // do not process side effects here.
            // raise InvoiceFinalizedDomainEvent event

            InvoiceFinalizedDomainEventPayload dePayload = new ()
            { 
                DateOfFinalization = DateTime.Now ,
                Invoice = InvoiceModel.ToDTO(invoiceModel),
                InvoiceItems =  InvoiceItemModel.ToDTO(invoiceModel.Items),
            };

            InvoiceFinalizedDomainEvent de = new(dePayload);
            
            _eventBus.Handle(de);

            /*
            foreach (InvoiceItemModel item in invoiceModel.Items)
            {
                _inventoryItemRepo.ReduceQuantity(
                    articleId: item.ArticleId,
                    quantity: item.Quantity);
            }        

            // Do not forget!!!
            // Taxman will come!!!

            TaxMessageInvoiceStatuses taxMessageInvoiceStatus = _taxMessageCommonLogic.MapInvoiceStatus(
                invoiceStatus: invoiceModel.Status);

            TaxMessageDTO taxMessageDTO = new TaxMessageDTO()
            {
                CustomerId = invoiceModel.CustomerId,
                InvoiceNumber = invoiceModel.InvoiceNumber,
                PriceWithoutTax = invoiceModel.PriceWithoutTax,
                PriceWithTax = invoiceModel.PriceWithTax,
                TaxAtNormalRate = invoiceModel.TaxAtNormalRate,
                TaxAtReducedRate = invoiceModel.TaxAtReducedRate,
                Status = taxMessageInvoiceStatus
            };

            _taxMessageRepository.EnqueueForSending(
                taxMessageDTO);
            */

            // END DE

            // complete tran
            ts.Complete();

            return invoiceModel;
        }
    }
}
