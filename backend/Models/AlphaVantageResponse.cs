using System.Text.Json.Serialization;

namespace Backend.Models;

public class AlphaVantageResponse
{
    [JsonPropertyName("Time Series (15min)")]
    public Dictionary<string, TimeSeriesPoint>? TimeSeries{ get; set; }

    [JsonPropertyName("Error Message")]
    public string? ErrorMessage { get; set; }

    [JsonPropertyName("Information")]
    public string? Information { get; set; }
}