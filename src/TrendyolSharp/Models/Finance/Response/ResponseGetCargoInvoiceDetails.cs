﻿using System.Collections.Generic;
using TrendyolSharp.Models.Marketplace;

namespace TrendyolSharp.Models.Finance.Response
{
  public sealed class ResponseGetCargoInvoiceDetails
  {
    public long Page { get; set; }
    public long Size { get; set; }
    public long TotalPages { get; set; }
    public long TotalElements { get; set; }
    public List<InvoiceShipmentPackage> Content { get; set; }
  }
}