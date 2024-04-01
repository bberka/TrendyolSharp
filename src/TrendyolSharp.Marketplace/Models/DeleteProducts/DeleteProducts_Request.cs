namespace TrendyolSharp.Marketplace.Models.DeleteProducts;

public class DeleteProducts_Request
{
  [Required]
  public List<DeleteProducts_Item> Items { get; set; }
}