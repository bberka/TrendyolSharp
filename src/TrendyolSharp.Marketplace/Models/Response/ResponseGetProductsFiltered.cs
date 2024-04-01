using TrendyolSharp.Marketplace.Models.Base;

namespace TrendyolSharp.Marketplace.Models.Response;

public sealed class ResponseGetProductsFiltered
{
  public int TotalElements { get; set; }
  public int TotalPages { get; set; }
  public int Page { get; set; }
  public int Size { get; set; }
  public List<ProductContent> Content { get; set; }
}