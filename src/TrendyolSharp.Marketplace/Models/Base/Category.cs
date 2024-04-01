namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class Category
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }

  public int ParentId { get; set; }

  public List<SubCategory> SubCategories { get; set; }
}