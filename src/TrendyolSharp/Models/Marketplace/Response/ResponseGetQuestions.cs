using System.Collections.Generic;

namespace TrendyolSharp.Models.Marketplace.Response
{
  public sealed class ResponseGetQuestions
  {
    public List<Question> Content { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    public long TotalElements { get; set; }
    public long TotalPages { get; set; }
  }
}