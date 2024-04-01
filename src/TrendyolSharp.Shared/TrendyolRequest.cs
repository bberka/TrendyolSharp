using System.Net.Http.Json;
using System.Text;
using TrendyolSharp.Shared.Extensions;
using TrendyolSharp.Shared.Models;

namespace TrendyolSharp.Shared;

public sealed class TrendyolRequest
{
  private readonly HttpClient _httpClient;
  private readonly string _url;
  private readonly Dictionary<string, string>? _headers;


  public TrendyolRequest(HttpClient httpClient, string url, Dictionary<string, string>? headers = null) {
    _httpClient = httpClient;
    _url = url;
    _headers = headers;
  }

  public async Task<ResponseInformation> SendGetRequestAsync() {
    var request = new HttpRequestMessage(HttpMethod.Get, _url);
    if (_headers is not null) {
      foreach (var (key, value) in _headers) {
        request.Headers.Add(key, value);
      }
    }

    var response = await _httpClient.SendAsync(request);
    var content = await response.Content.ReadAsStringAsync();
    var statusCode = response.StatusCode;
    var reasonPhrase = response.ReasonPhrase;
    var headers = response.Headers.ToDictionary(x => x.Key, x => x.Value.First());
    var isSuccessStatusCode = response.IsSuccessStatusCode;
    return new ResponseInformation(isSuccessStatusCode, (int)statusCode, reasonPhrase, content, headers);
  }

  public async Task<ResponseInformation> SendPostRequestAsync(object? data = null) {
    var request = new HttpRequestMessage(HttpMethod.Post, _url);

    if (data is not null) {
      request.Content = new StringContent(data.ToJsonString(), encoding: Encoding.Default, mediaType: "application/json");
    }

    if (_headers is not null) {
      foreach (var (key, value) in _headers) {
        request.Headers.Add(key, value);
      }
    }


    var response = await _httpClient.SendAsync(request);
    var content = await response.Content.ReadAsStringAsync();
    var statusCode = response.StatusCode;
    var reasonPhrase = response.ReasonPhrase;
    var responseHeaders = response.Headers.ToDictionary(x => x.Key, x => x.Value.First());
    var isSuccessStatusCode = response.IsSuccessStatusCode;
    return new ResponseInformation(isSuccessStatusCode, (int)statusCode, reasonPhrase, content, responseHeaders);
  }
  
  public async Task<ResponseInformation> SendPutRequestAsync(object? data = null) {
    var request = new HttpRequestMessage(HttpMethod.Put, _url);

    if (data is not null) {
      request.Content = new StringContent(data.ToJsonString(), encoding: Encoding.Default, mediaType: "application/json");
    }

    if (_headers is not null) {
      foreach (var (key, value) in _headers) {
        request.Headers.Add(key, value);
      }
    }

    var response = await _httpClient.SendAsync(request);
    var content = await response.Content.ReadAsStringAsync();
    var statusCode = response.StatusCode;
    var reasonPhrase = response.ReasonPhrase;
    var responseHeaders = response.Headers.ToDictionary(x => x.Key, x => x.Value.First());
    var isSuccessStatusCode = response.IsSuccessStatusCode;
    return new ResponseInformation(isSuccessStatusCode, (int)statusCode, reasonPhrase, content, responseHeaders);
  }
  
  public async Task<ResponseInformation> SendDeleteRequestAsync() {
    var request = new HttpRequestMessage(HttpMethod.Delete, _url);
    if (_headers is not null) {
      foreach (var (key, value) in _headers) {
        request.Headers.Add(key, value);
      }
    }

    var response = await _httpClient.SendAsync(request);
    var content = await response.Content.ReadAsStringAsync();
    var statusCode = response.StatusCode;
    var reasonPhrase = response.ReasonPhrase;
    var headers = response.Headers.ToDictionary(x => x.Key, x => x.Value.First());
    var isSuccessStatusCode = response.IsSuccessStatusCode;
    return new ResponseInformation(isSuccessStatusCode, (int)statusCode, reasonPhrase, content, headers);
  }
}