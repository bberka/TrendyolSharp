namespace TrendyolSharp.Shared.Models;

public record TrendyolApiResult(
  bool IsSuccessStatusCode,
  int StatusCode,
  string? ReasonPhrase,
  string Content,
  IReadOnlyDictionary<string, string> Headers,
  string RequestUrl,
  string? RequestJsonBody = null)
{
  public TrendyolApiResult<T> WithData<T>(T data) => new(IsSuccessStatusCode,
                                                         StatusCode,
                                                         ReasonPhrase,
                                                         Content,
                                                         Headers,
                                                         data,
                                                         RequestUrl,
                                                         RequestJsonBody);
}

public record TrendyolApiResult<T>(
  bool IsSuccessStatusCode,
  int StatusCode,
  string? ReasonPhrase,
  string Content,
  IReadOnlyDictionary<string, string> Headers,
  T Data,
  string RequestUrl,
  string? RequestJsonBody = null)
{
  public TrendyolApiResult ToApiResult() {
    return new TrendyolApiResult(IsSuccessStatusCode,
                                 StatusCode,
                                 ReasonPhrase,
                                 Content,
                                 Headers,
                                 RequestUrl,
                                 RequestJsonBody);
  }
}