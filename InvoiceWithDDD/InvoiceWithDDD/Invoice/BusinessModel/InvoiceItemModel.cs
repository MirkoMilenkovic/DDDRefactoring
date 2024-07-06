using InvoiceWithDDD.Common;
using InvoiceWithDDD.Invoice.DTO;
using InvoiceWithDDD.MasterData.Articles;

namespace InvoiceWithDDD.Invoice.BusinessModel
{
    public class InvoiceItemModel : BaseModel
    {
        // DDD
        // ugly constructor is necessary, since we can not use required.
        public InvoiceItemModel(
            int id,
            EntityStates entityState,
            InvoiceModel invoice,
            int articleId,
            int quantity,
            decimal unitPriceWithoutTax,
            decimal priceWithoutTax,
            decimal tax,
            decimal priceWithTax,
            decimal taxRate)
            : base(id, entityState)
        {
            Invoice = invoice;
            ArticleId = articleId;
            Quantity = quantity;
            UnitPriceWithoutTax = unitPriceWithoutTax;
            PriceWithoutTax = priceWithoutTax;
            Tax = tax;
            PriceWithTax = priceWithTax;
            TaxRate = taxRate;
        }
        // END DDD

        public InvoiceModel Invoice { get; private set; }

        public int ArticleId { get; private set; }

        public int Quantity { get; private set; }

        public decimal UnitPriceWithoutTax { get; private set; }

        public decimal PriceWithoutTax { get; private set; }

        public decimal Tax { get; private set; }

        public decimal PriceWithTax { get; private set; }

        public decimal TaxRate { get; private set; }

        // Note that I'm not sending Article, but its properties.
        public void CalculateMoney(
            ArticleDTO.TaxGroup articleTaxGroup,
            decimal articleUnitPriceWithoutTax)
        {
            switch (articleTaxGroup)
            {
                case ArticleDTO.TaxGroup.Normal:
                    TaxRate = TaxConstants.TAX_RATE_NORMAL;
                    break;
                case ArticleDTO.TaxGroup.Reduced:
                    TaxRate = TaxConstants.TAX_RATE_REDUCED;
                    break;
                default:
                    throw new Exception($"Taxman came up with unexpected tax group: {articleTaxGroup}");
            }

            UnitPriceWithoutTax = articleUnitPriceWithoutTax;

            CalculateMoney();
        }

        public void CalculateMoney()
        {
            if (TaxRate == 0)
            {
                throw new InvalidOperationException("TaxRate is not determined. Call other method, that accepts TaxGroup");
            }

            PriceWithoutTax = UnitPriceWithoutTax * Quantity;

            Tax = TaxRate * PriceWithoutTax;

            PriceWithTax = PriceWithoutTax + Tax;

            // do not forget this!!!
            EntityState = EntityStates.Updated;
        }

        #region factory methods

        public static InvoiceItemModel FromDTO(
            InvoiceItemDTO itemDTO,
            InvoiceModel invoiceModel,
            EntityStates entityState)
        {
            // DDD
            InvoiceItemModel itemModel = new InvoiceItemModel(
                id: itemDTO.Id,
                entityState: entityState,
                articleId: itemDTO.ArticleId,
                invoice: invoiceModel,
                unitPriceWithoutTax: itemDTO.UnitPriceWithoutTax,
                priceWithoutTax: itemDTO.PriceWithoutTax,
                priceWithTax: itemDTO.PriceWithTax,
                quantity: itemDTO.Quantity,
                tax: itemDTO.Tax,
                taxRate: itemDTO.TaxRate
            );
            // END DDD

            /*
            InvoiceItemModel itemModel = new InvoiceItemModel()
            {
                Id = itemDTO.Id,
                EntityState = entityState,
                ArticleId = itemDTO.ArticleId,
                Invoice = invoiceModel,
                UnitPriceWithoutTax = itemDTO.UnitPriceWithoutTax,
                PriceWithoutTax = itemDTO.PriceWithoutTax,
                PriceWithTax = itemDTO.PriceWithTax,
                Quantity = itemDTO.Quantity,
                Tax = itemDTO.Tax,
                TaxRate = itemDTO.TaxRate,
            };
            */

            return itemModel;
        }

        public static InvoiceItemDTO ToDTO(
            InvoiceItemModel itemModel)
        {
            InvoiceItemDTO itemDto = new InvoiceItemDTO()
            {
                ArticleId = itemModel.ArticleId,
                Id = itemModel.Id,
                InvoiceId = itemModel.Invoice.Id, // Note this.
                UnitPriceWithoutTax = itemModel.UnitPriceWithoutTax,
                PriceWithoutTax = itemModel.PriceWithoutTax,
                PriceWithTax = itemModel.PriceWithTax,
                Quantity = itemModel.Quantity,
                Tax = itemModel.Tax,
                TaxRate = itemModel.TaxRate,
            };

            return itemDto;
        }

        #endregion
    }
}
