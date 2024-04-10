using System.Collections;
using System.Web;

namespace TrendyolSharp.Shared.Utils;

public static class UtilUrl
{
  public static string BuildUrl(this string url, params KeyValuePair<string, object?>[] queryParams) {
    var uriBuilder = new UriBuilder(url);
    var query = HttpUtility.ParseQueryString(uriBuilder.Query);

    foreach (var (key, value) in queryParams) {
      if (value is null) {
        continue;
      }

      if (value is IEnumerable enumerable) {
        foreach (var item in enumerable) {
          query.Add(key, item.ToString());
        }
      }
      else if (value is bool) {
        query[key] = value.ToString()?.ToLower();
      }
      else {
        query[key] = value.ToString();
      }
    }

    uriBuilder.Query = query.ToString();
    return uriBuilder.ToString();
  }
}