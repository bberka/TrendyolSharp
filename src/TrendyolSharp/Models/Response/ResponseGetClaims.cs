using System.Collections.Generic;
using TrendyolSharp.Models.Base;

namespace TrendyolSharp.Models.Response
{
  public sealed class ResponseGetClaims
  {
    public long TotalElements { get; set; }
    public long TotalPages { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    public List<Claim> Content { get; set; }
  }
}