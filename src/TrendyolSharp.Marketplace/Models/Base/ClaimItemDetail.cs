﻿namespace TrendyolSharp.Marketplace.Models.Base;

public class ClaimItemDetail
{
  public string Id { get; set; }
  public int OrderLineItemId { get; set; }
  public ClaimItemReason CustomerClaimItemReason { get; set; }
  public ClaimItemReason TrendyolClaimItemReason { get; set; }
  public ClaimItemStatus ClaimItemStatus { get; set; }
  public string Note { get; set; }
  public string CustomerNote { get; set; }
  public bool Resolved { get; set; }
}