namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class SubCategory
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }

  public int ParentId { get; set; }

  public List<SubCategory> SubCategories { get; set; }
}