namespace TrendyolSharp.Marketplace.Models.UpdateProduct;

public class UpdateProduct_Request
{
  [Required]
  public List<UpdateProduct_Item> Items { get; set; }
}