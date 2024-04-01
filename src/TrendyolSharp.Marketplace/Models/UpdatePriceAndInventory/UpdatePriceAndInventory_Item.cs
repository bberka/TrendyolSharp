namespace TrendyolSharp.Marketplace.Models.UpdatePriceAndInventory;

public class UpdatePriceAndInventory_Item
{
  [Required]
  [StringLength(40)]
  public string Barcode { get; set; }

  [Required]
  public int Quantity { get; set; }

  [Required]
  public decimal SalePrice { get; set; }

  [Required]
  public decimal ListPrice { get; set; }
}
