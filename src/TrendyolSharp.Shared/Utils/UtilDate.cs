namespace TrendyolSharp.Shared.Utils;

public static class UtilDate
{
  // public static long ToTimestamp(this DateTime date)
  // {
  //   return new DateTimeOffset(date).ToUnixTimeSeconds();
  // }
  //
  public static long ToTimestampMiliseconds(this DateTime dateTime)
  {
    var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    var timeSpan = dateTime.ToUniversalTime() - unixEpoch;
    return (long)timeSpan.TotalMilliseconds;
  }
}