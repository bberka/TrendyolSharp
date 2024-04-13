using System;
using TrendyolSharp.Abstract;
using TrendyolSharp.Extensions;
using TrendyolSharp.Models;

namespace TrendyolSharp.Exceptions
{
  public class RequestFailedException : TrendyolException
  {
    public RequestFailedException(string requestKey, TrendyolApiResult trendyolApiResult) :
      base($"Request failed for {requestKey}. Response: {trendyolApiResult.ToJsonString()}") { }

    public RequestFailedException(string requestKey, Exception exception) : base($"Request failed for {requestKey}, check inner exception", exception) { }
  }
}