using System.Collections.Generic;

namespace TrendyolSharp.Models.Marketplace.Request
{
  public sealed class RequestCreateProducts
  {
    public List<CreateProduct> Items { get; set; }
  }
}