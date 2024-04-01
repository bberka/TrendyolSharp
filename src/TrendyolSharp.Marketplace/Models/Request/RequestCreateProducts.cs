using TrendyolSharp.Marketplace.Models.Base;

namespace TrendyolSharp.Marketplace.Models.Request;

public sealed class RequestCreateProducts
{
  [Required]
  public List<CreateProduct> Items { get; set; }
}