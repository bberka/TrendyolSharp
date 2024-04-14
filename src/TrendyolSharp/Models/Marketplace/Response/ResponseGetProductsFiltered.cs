using System.Collections.Generic;

namespace TrendyolSharp.Models.Marketplace.Response
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