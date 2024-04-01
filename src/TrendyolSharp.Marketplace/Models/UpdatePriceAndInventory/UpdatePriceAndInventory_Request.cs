namespace TrendyolSharp.Marketplace.Models.UpdatePriceAndInventory;

public class UpdatePriceAndInventory_Request
{
  [Required]
  public List<UpdatePriceAndInventory_Item> Items { get; set; }
}