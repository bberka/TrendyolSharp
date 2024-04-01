using Newtonsoft.Json;

namespace TrendyolSharp.Shared.Extensions;

public static class JsonExtensions
{
  public static string ToJsonString(this object obj) {
    return JsonConvert.SerializeObject(obj, Formatting.Indented);
  }

  public static string ToJsonString(this object obj, Formatting formatting) {
    return JsonConvert.SerializeObject(obj, formatting);
  }

  public static T ToObject<T>(this string json) {
    return JsonConvert.DeserializeObject<T>(json);
  }

  public static T ToObject<T>(this string json, JsonSerializerSettings settings) {
    return JsonConvert.DeserializeObject<T>(json, settings);
  }
}