using System;
using TrendyolSharp.Enum;

namespace TrendyolSharp.Models.Finance.Filter
{
  public sealed class FilterGetSettlements
  {
    public SettlementsTransactionType TransactionType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? Page { get; set; }
    public int? Size { get; set; }
  }
}