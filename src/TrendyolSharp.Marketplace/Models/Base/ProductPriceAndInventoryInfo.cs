namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class ProductPriceAndInventoryInfo
{
  [Required]
  [StringLength(40)]
  public string Barcode { get; set; }

  [Required]
  public long Quantity { get; set; }

  [Required]
  public decimal SalePrice { get; set; }

  [Required]
  public decimal ListPrice { get; set; }
}