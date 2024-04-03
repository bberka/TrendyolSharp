namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class OrderLine
{
  public int Id { get; set; }
  public string ProductName { get; set; }
  public string Barcode { get; set; }
  public string MerchantSku { get; set; }
  public string ProductColor { get; set; }
  public string ProductSize { get; set; }
  public double Price { get; set; }
  public int VatBaseAmount { get; set; }
  public int SalesCampaignId { get; set; }
  public string ProductCategory { get; set; }
}