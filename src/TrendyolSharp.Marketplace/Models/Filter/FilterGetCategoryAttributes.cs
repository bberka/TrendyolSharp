namespace TrendyolSharp.Marketplace.Models.Filter;

public class FilterGetCategoryAttributes
{
  public string? Name { get; set; }
  public string? DisplayName { get; set; }
  public int? AttributeId { get; set; }
  public string? AttributeName { get; set; }
  public bool? AllowCustom { get; set; }
  public bool? Required { get; set; }
  public bool? Slicer { get; set; }
  public bool? Varianter { get; set; }
  public int? AttributeValueId { get; set; }
  public string? AttributeValueName { get; set; }
}