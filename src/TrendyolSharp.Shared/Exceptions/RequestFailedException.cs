using TrendyolSharp.Shared.Abstract;
using TrendyolSharp.Shared.Common;
using TrendyolSharp.Shared.Extensions;
using TrendyolSharp.Shared.Models;

namespace TrendyolSharp.Shared.Exceptions;

public class RequestFailedException : TrendyolException
{
  public RequestFailedException(string requestKey, ResponseInformation responseInformation) :
    base($"Request failed for {requestKey}. Response: {responseInformation.ToJsonString()}") { }

  public RequestFailedException(string requestKey, Exception exception) : base($"Request failed for {requestKey}, check inner exception", exception) { }
}