namespace TrendyolSharp.Shared.Models;

public record TrendyolApiResult(
  bool IsSuccessStatusCode,
  int StatusCode,
  string? ReasonPhrase,
  string Content,
  IReadOnlyDictionary<string, string> Headers)
{
  public TrendyolApiResult<T> WithData<T>(T data) => new(IsSuccessStatusCode,
                                                         StatusCode,
                                                         ReasonPhrase,
                                                         Content,
                                                         Headers,
                                                         data);
}

public record TrendyolApiResult<T>(
  bool IsSuccessStatusCode,
  int StatusCode,
  string? ReasonPhrase,
  string Content,
  IReadOnlyDictionary<string, string> Headers,
  T Data);