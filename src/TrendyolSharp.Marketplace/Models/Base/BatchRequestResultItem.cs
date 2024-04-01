namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class BatchRequestResultItem
{
  [Required]
  public BatchRequestResultRequestItem RequestItem { get; set; }

  [Required]
  public string Status { get; set; }

  [Required]
  public List<object> FailureReasons { get; set; }
}