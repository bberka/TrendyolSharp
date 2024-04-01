namespace TrendyolSharp.Marketplace.Models.DeleteProducts;

public class DeleteProducts_Item
{
  [Required]
  [StringLength(40)]
  public string Barcode { get; set; }
}
