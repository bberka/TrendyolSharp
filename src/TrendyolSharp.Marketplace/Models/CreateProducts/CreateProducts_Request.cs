namespace TrendyolSharp.Marketplace.Models.CreateProducts;

public sealed class CreateProducts_Request
{
  [Required]
  public List<CreateProducts_Item> Items { get; set; }
}