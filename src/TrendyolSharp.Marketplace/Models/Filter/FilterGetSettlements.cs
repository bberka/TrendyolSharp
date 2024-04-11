using TrendyolSharp.Marketplace.Enum;

namespace TrendyolSharp.Marketplace.Models.Filter;

public sealed class FilterGetSettlements
{
  public TransactionType TransactionType { get; set; }
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }
  public int? Page { get; set; }
  public int? Size { get; set; }
}