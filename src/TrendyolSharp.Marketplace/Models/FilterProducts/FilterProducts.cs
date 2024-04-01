namespace TrendyolSharp.Marketplace.Models.FilterProducts;

public class FilterProducts
{
  public int TotalElements { get; set; }
  public int TotalPages { get; set; }
  public int Page { get; set; }
  public int Size { get; set; }
  public List<FilterProducts_Content> Content { get; set; }
}