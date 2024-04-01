namespace TrendyolSharp.Marketplace.Models.GetBrands;

public class GetBrands_Brand
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }
}