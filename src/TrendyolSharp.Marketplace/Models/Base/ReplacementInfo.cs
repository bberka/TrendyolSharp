﻿namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class ReplacementInfo
{
  public long CargoTrackingNumber { get; set; }
  public string CargoProviderName { get; set; }
  public string CargoSenderNumber { get; set; }
  public string CargoTrackingLink { get; set; }
  public int Packageid { get; set; }
  public List<Guid> Items { get; set; }
}