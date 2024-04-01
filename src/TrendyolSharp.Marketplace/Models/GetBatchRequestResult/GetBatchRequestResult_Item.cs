namespace TrendyolSharp.Marketplace.Models.GetBatchRequestResult;

public class GetBatchRequestResult_Item
{
  [Required]
  public GetBatchRequestResult_RequestItem RequestItem { get; set; }

  [Required]
  public string Status { get; set; }

  [Required]
  public List<object> FailureReasons { get; set; }
}
