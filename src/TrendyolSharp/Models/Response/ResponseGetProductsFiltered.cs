using System.Collections.Generic;
using TrendyolSharp.Models.Base;

namespace TrendyolSharp.Models.Response
{
  public sealed class ResponseGetProductsFiltered
  {
    public long TotalElements { get; set; }
    public long TotalPages { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    public List<ProductContent> Content { get; set; }
  }
}