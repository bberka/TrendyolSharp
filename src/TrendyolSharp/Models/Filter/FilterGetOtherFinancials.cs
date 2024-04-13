using System;
using TrendyolSharp.Enum;

namespace TrendyolSharp.Models.Filter
{
  public sealed class FilterGetOtherFinancials
  {
    public OtherFinancialTransactionType TransactionType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? Page { get; set; }
    public int? Size { get; set; }
  }
}