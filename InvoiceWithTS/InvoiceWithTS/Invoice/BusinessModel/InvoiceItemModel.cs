using InvoiceWithTS.Common;
using InvoiceWithTS.Invoice.DTO;

namespace InvoiceWithTS.Invoice.BusinessModel
{
    public class InvoiceItemModel
    {
        public required int Id { get; set; }

        public required EntityStates EntityState { get; set; }

        public required InvoiceModel Invoice { get; set; }

        public required int ArticleId { get; set; }

        public required int Quantity { get; set; }

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
