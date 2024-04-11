using TrendyolSharp.Marketplace.Models.Base;

namespace TrendyolSharp.Marketplace.Models.Response;

public sealed class ResponseGetSettlements
{
  public int Page { get; set; }
  public int Size { get; set; }
  public long TotalPages { get; set; }
  public long TotalElements { get; set; }
  public List<TransactionData> Content { get; set; }
}