namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class ProductBarcode
{
  [Required]
  [StringLength(40)]
  public string Barcode { get; set; }
}