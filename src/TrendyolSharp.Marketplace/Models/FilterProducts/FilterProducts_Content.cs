namespace TrendyolSharp.Marketplace.Models.FilterProducts;

public class FilterProducts_Content
{
    [Required]
    public string Id { get; set; }
    [Required]
    public bool Approved { get; set; }
    [Required]
    public bool Archived { get; set; }
    public int ProductCode { get; set; }
    [Required]
    public string BatchRequestId { get; set; }
    [Required]
    public int SupplierId { get; set; }
    [Required]
    public long CreateDateTime { get; set; }
    [Required]
    public long LastUpdateDate { get; set; }
    public string Gender { get; set; }
    [Required]
    public string Brand { get; set; }
    [Required]
    public string Barcode { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string CategoryName { get; set; }
    [Required]
    public string ProductMainId { get; set; }
    [Required]
    public string Description { get; set; }
    public string StockUnitType { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public decimal ListPrice { get; set; }
    [Required]
    public decimal SalePrice { get; set; }
    [Required]
    public int VatRate { get; set; }
    [Required]
    public decimal DimensionalWeight { get; set; }
    [Required]
    public string StockCode { get; set; }
    [Required]
    public FilterProducts_DeliveryOption DeliveryOption { get; set; }
    [Required]
    public List<FilterProducts_Image> Images { get; set; }
    [Required]
    public List<FilterProducts_Attribute> Attributes { get; set; }
    public string PlatformListingId { get; set; }
    [Required]
    public string StockId { get; set; }
    [Required]
    public bool HasActiveCampaign { get; set; }
    [Required]
    public bool Locked { get; set; }
    [Required]
    public int ProductContentId { get; set; }
    [Required]
    public int PimCategoryId { get; set; }
    [Required]
    public int BrandId { get; set; }
    [Required]
    public int Version { get; set; }
    [Required]
    public string Color { get; set; }
    [Required]
    public string Size { get; set; }
    [Required]
    public bool LockedByUnSuppliedReason { get; set; }
    [Required]
    public bool Onsale { get; set; }
    [Required]
    public string ProductUrl { get; set; }
}