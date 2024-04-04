using TrendyolSharp.Marketplace.Models.Base;

namespace TrendyolSharp.Marketplace.Models.Response;

public sealed class ResponseGetQuestions
{
  public List<Question> Content { get; set; }
  public int Page { get; set; }
  public int Size { get; set; }
  public int TotalElements { get; set; }
  public int TotalPages { get; set; }
}