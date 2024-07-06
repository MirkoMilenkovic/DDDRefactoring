using InvoiceWithDDD.Common;
using InvoiceWithDDD.Invoice.DTO;
using static InvoiceWithDDD.Invoice.DTO.InvoiceDTO;

namespace InvoiceWithDDD.Invoice.BusinessModel
{
    public class InvoiceModel : BaseModel
    {
        public InvoiceModel(
            int id,
            EntityStates entityState,
            string invoiceNumber,
            int customerId,
            DateTime invoiceDate,
            InvoiceStatuses status,
            decimal priceWithoutTax,
            decimal taxAtNormalRate,
            decimal taxAtReducedRate,
            decimal priceWithTax)
            : base(id, entityState)
        {
            InvoiceNumber = invoiceNumber;
            CustomerId = customerId;
            InvoiceDate = invoiceDate;
            Status = status;
            PriceWithoutTax = priceWithoutTax;
            TaxAtNormalRate = taxAtNormalRate;
            TaxAtReducedRate = taxAtReducedRate;
            PriceWithTax = priceWithTax;
        }

        public string InvoiceNumber { get; private set; }

        public int CustomerId { get; private set; }

        public DateTime InvoiceDate { get; private set; }

        public InvoiceStatuses Status { get; private set; }

        public decimal PriceWithoutTax { get; private set; } = 0M;

        public decimal TaxAtNormalRate { get; private set; } = 0M;

        public decimal TaxAtReducedRate { get; private set; } = 0M;

        public decimal PriceWithTax { get; private set; } = 0M;

        public List<InvoiceItemModel> Items { get; private set; } = new List<InvoiceItemModel>();

        public static InvoiceModel FromDTO(
            InvoiceDTO invoiceDto,
            IEnumerable<InvoiceItemDTO> itemDtoList,
            EntityStates entityState)
        {
            // DDD
            InvoiceModel invoiceModel = new InvoiceModel(
                id: invoiceDto.Id,
                entityState: entityState,
                customerId: invoiceDto.CustomerId,
                invoiceDate: invoiceDto.InvoiceDate,
                invoiceNumber: invoiceDto.InvoiceNumber,
                priceWithoutTax: invoiceDto.PriceWithoutTax,
                priceWithTax: invoiceDto.PriceWithTax,
                status: invoiceDto.Status,
                taxAtNormalRate: invoiceDto.TaxAtNormalRate,
                taxAtReducedRate: invoiceDto.TaxAtReducedRate
            );
            // END DDD

            /*
            InvoiceModel invoiceModel = new InvoiceModel()
            {
                Id = invoiceDto.Id,
                EntityState = entityState,
                CustomerId = invoiceDto.CustomerId,
                InvoiceDate = invoiceDto.InvoiceDate,
                InvoiceNumber = invoiceDto.InvoiceNumber,
                PriceWithoutTax = invoiceDto.PriceWithoutTax,
                PriceWithTax = invoiceDto.PriceWithTax,
                Status = invoiceDto.Status,
                TaxAtNormalRate = invoiceDto.TaxAtNormalRate,
                TaxAtReducedRate = invoiceDto.TaxAtReducedRate,
            };
            */

            foreach (var itemDto in itemDtoList)
            {
                InvoiceItemModel itemModel = InvoiceItemModel.FromDTO(
                    itemDTO: itemDto,
                    invoiceModel: invoiceModel,
                    entityState: entityState);

                invoiceModel.Items.Add(itemModel);
            }

            return invoiceModel;
        }

        #region business logic

        public void ResetStateToLoaded()
        {
            EntityState = EntityStates.Loaded;

            foreach (InvoiceItemModel item in Items)
            {
                item.ResetStateToLoaded();
            }
        }

        public InvoiceItemModel AddItem(
            int articleId,
            int quantity)
        {
            InvoiceItemModel item = InvoiceItemModel.CreateNew(
                invoice: this,
                articleId: articleId,
                quantity: quantity);

            this.Items.Add(
                item);

            // Money has changed.
            CalculateMoney();

            // Return to user.
            return item;
        }

        /// <summary>
        /// - find item
        /// - call item.UpdateQuantity (see what it does)
        /// - CalculateMoney
        /// Voila, we have valid invoice!!!
        /// - return item, so UI has something to show.
        /// </summary>
        public InvoiceItemModel UpdateItem(
            int itemId,
            int quantity)
        {
            InvoiceItemModel? item = Items.FirstOrDefault(x => x.Id == itemId);

            if (item == null)
            {
                throw new Exception($"ItemId: {itemId} not found");
            }

            // Update quantity, calculate money, and set EntityState to Updated.
            item.Update(quantity);

            // Money has changed on Invoice also.
            CalculateMoney();

            // No need to set EntityState, CalculateMoney has determined if it should be Updated.

            return item;
        }

        #endregion

        public void CalculateMoney()           
        {
            // reset
            PriceWithoutTax = 0M;
            PriceWithTax = 0M;
            TaxAtNormalRate = 0M;
            TaxAtReducedRate = 0M;

            foreach (InvoiceItemModel item in Items)
            {
                PriceWithoutTax += item.PriceWithoutTax;
                if (item.TaxRate == TaxConstants.TAX_RATE_NORMAL)
                {
                    TaxAtNormalRate += item.Tax;
                }
                else if (item.TaxRate == TaxConstants.TAX_RATE_REDUCED)
                {
                    TaxAtReducedRate += item.Tax;
                }
            }

            PriceWithTax = PriceWithoutTax
                + TaxAtReducedRate
                + TaxAtNormalRate;

            // do not forget this!!!
            EntityState = EntityStates.Updated;
        }

        #region factory methods

        public static InvoiceDTO ToDTO(
            InvoiceModel invoice)
        {
            InvoiceDTO invoiceDTO = new InvoiceDTO()
            {
                CustomerId = invoice.CustomerId,
                Id = invoice.Id,
                InvoiceDate = invoice.InvoiceDate,
                InvoiceNumber = invoice.InvoiceNumber,
                PriceWithoutTax = invoice.PriceWithoutTax,
                PriceWithTax = invoice.PriceWithTax,
                Status = invoice.Status,
                TaxAtNormalRate = invoice.TaxAtNormalRate,
                TaxAtReducedRate = invoice.TaxAtReducedRate
            };

            return invoiceDTO;
        }

        #endregion
    }
}
