using System.Collections.Generic;
using TrendyolSharp.Models.Base;

namespace TrendyolSharp.Models.Request
{
  public sealed class RequestUpdateProduct
  {
    public List<UpdateProduct> Items { get; set; }
  }
}