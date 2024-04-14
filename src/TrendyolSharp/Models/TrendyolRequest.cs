using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrendyolSharp.Abstract;
using TrendyolSharp.Extensions;

namespace TrendyolSharp.Models
{
  public sealed class TrendyolRequest
  {
    private readonly Dictionary<string, string> _headers;
    private readonly HttpClient _httpClient;
    private readonly string _url;


    public TrendyolRequest(HttpClient httpClient, string url, Dictionary<string, string> headers = null) {
      _httpClient = httpClient;
      _url = url;
      _headers = headers;
    }


    private async Task<TrendyolApiResult> SendRequestAsync(HttpMethod method,
                                                           Dictionary<string, string> headers = null,
                                                           Dictionary<string, string> formContent = null,
                                                           string jsonData = null) {
      var request = new HttpRequestMessage(method, _url);
      if (_headers != null)
        foreach (var (key, value) in _headers)
          request.Headers.Add(key, value);

      if (headers != null)
        foreach (var (key, value) in headers)
          request.Headers.Add(key, value);

      //These will only work for POST and PUT requests, in here we do not check it because it will throw anyway
      if (jsonData != null) request.Content = new StringContent(jsonData, Encoding.Default, "application/json");
      if (formContent != null) request.Content = new FormUrlEncodedContent(formContent);
      
      
      var response = await _httpClient.SendAsync(request);
      var content = await response.Content.ReadAsStringAsync();
      var statusCode = response.StatusCode;
      var reasonPhrase = response.ReasonPhrase;
      var responseHeaders = response.Headers.ToDictionary(x => x.Key, x => x.Value.First());
      var isSuccessStatusCode = response.IsSuccessStatusCode;
      return new TrendyolApiResult(isSuccessStatusCode,
                                   (int)statusCode,
                                   reasonPhrase,
                                   content,
                                   responseHeaders,
                                   _url,
                                   jsonData);
    }

    public async Task<TrendyolApiResult> SendGetRequestAsync(Dictionary<string, string> headers = null) {
      return await SendRequestAsync(HttpMethod.Get, headers: headers);
    }

    public async Task<TrendyolApiResult> SendPostRequestAsync(object data = null,
                                                              Dictionary<string, string> headers = null,
                                                              Dictionary<string, string> formContent = null) {
      return await SendRequestAsync(HttpMethod.Post, headers: headers, jsonData: data.ToJsonString(), formContent: formContent);
    }

    public async Task<TrendyolApiResult> SendPutRequestAsync(object data = null,
                                                             Dictionary<string, string> headers = null,
                                                             Dictionary<string, string> formContent = null) {
      return await SendRequestAsync(HttpMethod.Put, headers: headers, jsonData: data.ToJsonString(), formContent: formContent);
    }

    public async Task<TrendyolApiResult> SendDeleteRequestAsync(Dictionary<string, string> headers = null) {
      return await SendRequestAsync(HttpMethod.Delete, headers: headers);
    }
  }
}