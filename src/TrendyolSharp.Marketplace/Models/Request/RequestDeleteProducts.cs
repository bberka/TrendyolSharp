using TrendyolSharp.Marketplace.Models.Base;

namespace TrendyolSharp.Marketplace.Models.Request;

public class RequestDeleteProducts
{
  [Required]
  public List<ProductBarcode> Items { get; set; }
}