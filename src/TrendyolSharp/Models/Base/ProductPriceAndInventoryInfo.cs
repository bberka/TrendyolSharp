namespace TrendyolSharp.Models.Base
{
  public sealed class ProductPriceAndInventoryInfo
  {
    public string Barcode { get; set; }


    public long Quantity { get; set; }


    public decimal SalePrice { get; set; }


    public decimal ListPrice { get; set; }
  }
}