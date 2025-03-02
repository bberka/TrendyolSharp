using Newtonsoft.Json;

namespace TrendyolClient.Sharp.Extensions
{
  internal static class TrendyolJsonExtensions
  {
    private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
      ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver {
        NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
      },
      TypeNameHandling = TypeNameHandling.Auto,
      NullValueHandling = NullValueHandling.Ignore,
      Converters = { new Newtonsoft.Json.Converters.StringEnumConverter() },
      Formatting = Formatting.Indented
    };

    public static string ObjectToJsonString(this object obj) {
      return JsonConvert.SerializeObject(obj, Settings);
    }

    public static T JsonToObject<T>(this string json) {
      return JsonConvert.DeserializeObject<T>(json, Settings);
    }
  }
}