namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class SubCategory
{
  public long Id { get; set; }

  [Required]
  public string Name { get; set; }

  public long ParentId { get; set; }

  public List<SubCategory> SubCategories { get; set; }
}