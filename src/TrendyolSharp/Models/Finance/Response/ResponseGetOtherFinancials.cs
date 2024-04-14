﻿using System.Collections.Generic;

namespace TrendyolSharp.Models.Finance.Response
{
  public sealed class ResponseGetOtherFinancials
  {
    public int Page { get; set; }
    public int Size { get; set; }
    public long TotalPages { get; set; }
    public long TotalElements { get; set; }
    public List<OtherFinancialsTransactionData> Content { get; set; }
  }
}