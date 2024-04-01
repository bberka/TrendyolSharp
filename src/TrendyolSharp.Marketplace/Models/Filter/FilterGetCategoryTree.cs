namespace TrendyolSharp.Marketplace.Models.Filter;

public sealed class FilterGetCategoryTree
{
  public int? Id { get; set; }
  public int? ParentId { get; set; }
  public string Name { get; set; }
  public bool? SubCategories { get; set; }
}