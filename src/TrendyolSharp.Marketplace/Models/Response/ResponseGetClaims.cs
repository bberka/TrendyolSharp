
using TrendyolSharp.Marketplace.Models.Base;

namespace TrendyolSharp.Marketplace.Models.Response;

public sealed class ResponseGetClaims
{
  public int TotalElements { get; set; }
  public int TotalPages { get; set; }
  public int Page { get; set; }
  public int Size { get; set; }
  public List<Claim> Content { get; set; }
}