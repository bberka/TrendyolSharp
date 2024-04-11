using TrendyolSharp.Marketplace.Models.Base;

namespace TrendyolSharp.Marketplace.Models.Response;

public sealed class ResponseGetBatchRequestResult
{
  [Required]
  public string BatchRequestId { get; set; }

  [Required]
  public List<BatchRequestResultItem> Items { get; set; }

  [Required]
  public string Status { get; set; }

  [Required]
  public long CreationDate { get; set; }

  [Required]
  public long LastModification { get; set; }

  [Required]
  public string SourceType { get; set; }

  [Required]
  public long ItemCount { get; set; }
}