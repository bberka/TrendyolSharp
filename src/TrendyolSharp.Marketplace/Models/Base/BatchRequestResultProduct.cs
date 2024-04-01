namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class BatchRequestResultProduct
{
  public string Brand { get; set; }

  [Required]
  public string Barcode { get; set; }

  [Required]
  public string Title { get; set; }

  [Required]
  public string Description { get; set; }

  [Required]
  public string CategoryName { get; set; }

  [Required]
  public decimal ListPrice { get; set; }

  [Required]
  public decimal SalePrice { get; set; }

  [Required]
  public string CurrencyType { get; set; }

  [Required]
  public int VatRate { get; set; }

  [Required]
  public string CargoCompany { get; set; }

  [Required]
  public int Quantity { get; set; }

  [Required]
  public string StockCode { get; set; }

  [Required]
  public List<Image> Images { get; set; }

  [Required]
  public string ProductMainId { get; set; }

  [Required]
  public string Gender { get; set; }

  [Required]
  public decimal DimensionalWeight { get; set; }

  public List<object> Attributes { get; set; }

  [Required]
  public List<VariantAttribute> VariantAttributes { get; set; }
}