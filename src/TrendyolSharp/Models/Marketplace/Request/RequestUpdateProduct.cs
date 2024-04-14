using System.Collections.Generic;

namespace TrendyolSharp.Models.Marketplace.Request
{
  public sealed class RequestUpdateProduct
  {
    public List<UpdateProduct> Items { get; set; }
  }
}