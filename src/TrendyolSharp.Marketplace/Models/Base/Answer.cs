namespace TrendyolSharp.Marketplace.Models.Base;

public sealed class Answer
{
  public long CreationDate { get; set; }
  public bool HasPrivateInfo { get; set; }
  public long Id { get; set; }
  public string Reason { get; set; }
  public string Text { get; set; }
}