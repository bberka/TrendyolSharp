using System.Collections.Generic;

namespace TrendyolSharp.Models.Marketplace
{
  public sealed class BatchRequestResultItem
  {
    public BatchRequestResultRequestItem RequestItem { get; set; }


    public string Status { get; set; }


    public List<object> FailureReasons { get; set; }
  }
}