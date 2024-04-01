using TrendyolSharp.Marketplace.Models.Base;

namespace TrendyolSharp.Marketplace.Models.Request;

public sealed class RequestUpdatePriceAndInventory
{
  [Required]
  public List<ProductPriceAndInventoryInfo> Items { get; set; }
}