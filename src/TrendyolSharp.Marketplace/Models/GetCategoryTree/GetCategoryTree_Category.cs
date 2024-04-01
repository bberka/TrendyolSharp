namespace TrendyolSharp.Marketplace.Models.GetCategoryTree;

public class GetCategoryTree_Category
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }

  public int ParentId { get; set; }

  public List<GetCategoryTree_SubCategory> SubCategories { get; set; }
}