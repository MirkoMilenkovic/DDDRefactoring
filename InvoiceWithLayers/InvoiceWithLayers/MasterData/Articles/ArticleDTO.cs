using InvoiceWithLayers.Common;

namespace InvoiceWithLayers.MasterData.Articles
{
    /// <summary>
    /// Maps to Article table.
    /// </summary>
    public class ArticleDTO : BaseDBT
    {
        public enum TaxGroup
        {
            Normal,

            Reduced
        }

        public required string Code { get; set; }

        public required string Name { get; set; }

        public required decimal UnitPriceWithoutTax { get; set; }

        public required TaxGroup ArticleTaxGroup { get; set; }
    }
}
