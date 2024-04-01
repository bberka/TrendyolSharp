namespace TrendyolSharp.Marketplace.Models.GetBatchRequestResult;

public class GetBatchRequest_Result
{
  [Required]
  public string BatchRequestId { get; set; }

  [Required]
  public List<GetBatchRequestResult_Item> Items { get; set; }

  [Required]
  public string Status { get; set; }

  [Required]
  public long CreationDate { get; set; }

  [Required]
  public long LastModification { get; set; }

  [Required]
  public string SourceType { get; set; }

  [Required]
  public int ItemCount { get; set; }
}