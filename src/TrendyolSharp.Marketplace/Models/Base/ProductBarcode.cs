namespace TrendyolSharp.Marketplace.Models.Base;

public class ProductBarcode
{
  [Required]
  [StringLength(40)]
  public string Barcode { get; set; }
}