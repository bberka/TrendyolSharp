namespace TrendyolSharp.Marketplace.Models.GetCategoryAttributes;

public class GetCategoryAttributes_CategoryAttribute
{
  public int CategoryId { get; set; }

  public GetCategoryAttributes_Attribute Attribute { get; set; }

  public bool Required { get; set; }

  public bool AllowCustom { get; set; }

  public bool Varianter { get; set; }

  public bool Slicer { get; set; }

  public List<GetCategoryAttributes_AttributeValue> AttributeValues { get; set; }
}