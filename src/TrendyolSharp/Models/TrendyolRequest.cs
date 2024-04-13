using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

    public async Task<TrendyolApiResult> SendGetRequestAsync(Dictionary<string, string> headers = null) {
      var request = new HttpRequestMessage(HttpMethod.Get, _url);
      if (_headers != null)
        foreach (var (key, value) in _headers)
          request.Headers.Add(key, value);

      if (headers != null)
        foreach (var (key, value) in headers)
          request.Headers.Add(key, value);

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
                                   _url);
    }

    public async Task<TrendyolApiResult> SendPostRequestAsync(object data = null,
                                                              Dictionary<string, string> headers = null,
                                                              Dictionary<string, string> formContent = null) {
      var request = new HttpRequestMessage(HttpMethod.Post, _url);

      var json = data?.ToJsonString();
      if (data != null && json != null) request.Content = new StringContent(json, Encoding.Default, "application/json");

      if (_headers != null)
        foreach (var (key, value) in _headers)
          request.Headers.Add(key, value);


      if (headers != null)
        foreach (var (key, value) in headers)
          request.Headers.Add(key, value);

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
                                   json);
    }

    public async Task<TrendyolApiResult> SendPutRequestAsync(object data = null,
                                                             Dictionary<string, string> headers = null,
                                                             Dictionary<string, string> formContent = null) {
      var request = new HttpRequestMessage(HttpMethod.Put, _url);

      var json = data?.ToJsonString();
      if (data != null && json != null) request.Content = new StringContent(json, Encoding.Default, "application/json");

      if (_headers != null)
        foreach (var (key, value) in _headers)
          request.Headers.Add(key, value);

      if (headers != null)
        foreach (var (key, value) in headers)
          request.Headers.Add(key, value);

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
                                   json);
    }

    public async Task<TrendyolApiResult> SendDeleteRequestAsync() {
      var request = new HttpRequestMessage(HttpMethod.Delete, _url);
      if (_headers != null)
        foreach (var (key, value) in _headers)
          request.Headers.Add(key, value);

      var response = await _httpClient.SendAsync(request);
      var content = await response.Content.ReadAsStringAsync();
      var statusCode = response.StatusCode;
      var reasonPhrase = response.ReasonPhrase;
      var headers = response.Headers.ToDictionary(x => x.Key, x => x.Value.First());
      var isSuccessStatusCode = response.IsSuccessStatusCode;
      return new TrendyolApiResult(isSuccessStatusCode,
                                   (int)statusCode,
                                   reasonPhrase,
                                   content,
                                   headers,
                                   _url);
    }
  }
}