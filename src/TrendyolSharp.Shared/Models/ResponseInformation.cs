namespace TrendyolSharp.Shared.Models;

public record ResponseInformation(bool IsSuccessStatusCode, int StatusCode, string? ReasonPhrase, string Content, IReadOnlyDictionary<string, string> Headers);