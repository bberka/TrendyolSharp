using Newtonsoft.Json;

namespace TrendyolSharp.Extensions
{
  public static class JsonExtensions
  {
    private static JsonSerializerSettings _settings = new JsonSerializerSettings
    {
      ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
      {
        NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
      },
      TypeNameHandling = TypeNameHandling.Auto,
      NullValueHandling = NullValueHandling.Ignore,
      Converters = { new Newtonsoft.Json.Converters.StringEnumConverter() },
      Formatting = Formatting.Indented
    };
    public static string ToJsonString(this object obj) {
      return JsonConvert.SerializeObject(obj, _settings);
    }

    public static T ToObject<T>(this string json) {
      return JsonConvert.DeserializeObject<T>(json,_settings);
    }


  }
}