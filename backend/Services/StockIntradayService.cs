using System.Text.Json;
using Backend.Models;

namespace Backend.Services;

public class StockIntradayService(HttpClient httpClient, IConfiguration configuration)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = configuration;

    /// <summary>
    /// Retrieves intraday stock data for a given symbol.
    /// </summary>
    /// <param name="symbol">The stock symbol</param>
    /// <returns>A list of daily stock summaries containing average high, average low, and total volume for each day</returns>
    public async Task<List<DailyStockSummary>> GetIntradaySummaryAsync(string symbol)
    {
        // Get API key from config
        string? apiKey = _configuration.GetValue<string>("AlphaVantage:ApiKey");
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("AlphaVantage API key has not been configured. Please change it in appsettings.json");
        }

        // Build API request URL
        string QUERY_URL = $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={symbol}&interval=15min&apikey={apiKey}";

        // Fetch data from Alpha Vantage API
        HttpResponseMessage response = await _httpClient.GetAsync(QUERY_URL);
        response.EnsureSuccessStatusCode();

        // Parse JSON response
        string jsonContent = await response.Content.ReadAsStringAsync();
        AlphaVantageResponse? alphaVantageResponse = JsonSerializer.Deserialize<AlphaVantageResponse>(jsonContent);

        // Error handling
        if (!string.IsNullOrEmpty(alphaVantageResponse?.ErrorMessage))
        {
            throw new ArgumentException($"Invalid stock symbol: {symbol}. Please use valid stock symbols");
        }

        if (!string.IsNullOrEmpty(alphaVantageResponse?.Information))
        {
            throw new HttpRequestException($"Alpha Vantage note: {alphaVantageResponse.Information}");
        }

        if (alphaVantageResponse?.TimeSeries == null || alphaVantageResponse.TimeSeries.Count == 0)
        {
            throw new InvalidOperationException($"No time series data available for symbol: {symbol}");
        }

        // Group time series points by day
        var groupedByDay = alphaVantageResponse.TimeSeries
            .GroupBy(entry => DateTime.Parse(entry.Key).Date)
            .OrderByDescending(group => group.Key);

        var summaries = new List<DailyStockSummary>();

        foreach (var group in groupedByDay)
        {
            var date = group.Key;
            var lowAverage = Math.Round(group.Average(x => double.Parse(x.Value.Low ?? "0")), 4);
            var highAverage = Math.Round(group.Average(x => double.Parse(x.Value.High ?? "0")), 4);
            var volume = group.Sum(x => long.Parse(x.Value.Volume ?? "0"));

            summaries.Add(new DailyStockSummary
            {
                Day = date.ToString("yyyy-MM-dd"),
                LowAverage = lowAverage,
                HighAverage = highAverage,
                Volume = volume
            });
        }

        return summaries;
    }
}