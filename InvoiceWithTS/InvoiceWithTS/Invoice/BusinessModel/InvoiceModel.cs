using InvoiceWithTS.Common;
using InvoiceWithTS.Invoice.DTO;
using static InvoiceWithTS.Invoice.DTO.InvoiceDTO;

namespace InvoiceWithTS.Invoice.BusinessModel
{
    public class InvoiceModel
    {
        public required int Id { get; set; }

        public required EntityStates EntityState { get; set; }

        public required string InvoiceNumber { get; set; }

        public required int CustomerId { get; set; }

        public required DateTime InvoiceDate { get; set; }

        public required InvoiceStatuses Status { get; set; }

        public required decimal PriceWithoutTax { get; set; } = 0M;

        public required decimal TaxAtNormalRate { get; set; } = 0M;

        public required decimal TaxAtReducedRate { get; set; } = 0M;

        public required decimal PriceWithTax { get; set; } = 0M;

        public List<InvoiceItemModel> Items { get; set; } = new List<InvoiceItemModel>();

        public static InvoiceModel FromDTO(
            InvoiceDTO invoiceDto,
            IEnumerable<InvoiceItemDTO> itemDtoList,
            EntityStates entityState)
        {
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
    }
}
