using System;
using TrendyolClient.Sharp.Abstract;
using TrendyolClient.Sharp.Models;
using TrendyolClient.Sharp.Extensions;

namespace TrendyolClient.Sharp.Exceptions
{
  public class RequestFailedException : TrendyolException
  {
    public RequestFailedException(string requestKey, TrendyolApiResult trendyolApiResult) :
      base($"Request failed for {requestKey}. Response: {trendyolApiResult.ObjectToJsonString()}") { }

    public RequestFailedException(string requestKey, Exception exception) : base($"Request failed for {requestKey}, check inner exception", exception) { }
  }
}