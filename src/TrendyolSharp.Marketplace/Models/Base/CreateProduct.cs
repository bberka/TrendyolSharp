namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class CreateProduct
{
  [Required]
  [StringLength(40)]
  public string Barcode { get; set; }

  [Required]
  [StringLength(100)]
  public string Title { get; set; }

  [Required]
  [StringLength(40)]
  public string ProductMainId { get; set; }

  [Required]
  public int BrandId { get; set; }

  [Required]
  public int CategoryId { get; set; }

  [Required]
  public int Quantity { get; set; }

  [Required]
  [StringLength(100)]
  public string StockCode { get; set; }

  [Required]
  public decimal DimensionalWeight { get; set; }

  [Required]
  [StringLength(30000)]
  public string Description { get; set; }

  [Required]
  public string CurrencyType { get; set; }

  [Required]
  public decimal ListPrice { get; set; }

  [Required]
  public decimal SalePrice { get; set; }

  [Required]
  public int VatRate { get; set; }

  [Required]
  public int CargoCompanyId { get; set; }

  public int? ShipmentAddressId { get; set; }

  public int? ReturningAddressId { get; set; }

  public DeliveryOption DeliveryOption { get; set; }

  [Required]
  public List<Image> Images { get; set; }

  [Required]
  public List<Attribute> Attributes { get; set; }
}