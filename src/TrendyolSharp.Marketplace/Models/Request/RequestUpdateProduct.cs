using TrendyolSharp.Marketplace.Models.Base;

namespace TrendyolSharp.Marketplace.Models.Request;

public sealed class RequestUpdateProduct
{
  [Required]
  public List<UpdateProduct> Items { get; set; }
}