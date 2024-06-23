using InvoiceWithDDD.Common;
using InvoiceWithDDD.Invoice.DTO;

namespace InvoiceWithDDD.Invoice.BusinessModel
{
    public class InvoiceItemModel : BaseModel
    {
        public required InvoiceModel Invoice { get; set; }

        public required int ArticleId { get; set; }

        public required int Quantity { get; set; }

        public required decimal UnitPriceWithoutTax { get; set; }

        public required decimal PriceWithoutTax { get; set; }

        public required decimal Tax { get; set; }

        public required decimal PriceWithTax { get; set; }

        public required decimal TaxRate { get; set; }

        public static InvoiceItemModel FromDTO(
            InvoiceItemDTO itemDTO,
            InvoiceModel invoiceModel,
            EntityStates entityState)
        {
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
    }
}
