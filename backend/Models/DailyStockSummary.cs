namespace Backend.Models;

public class DailyStockSummary
{
    public string Day { get; set; } = string.Empty;
    public double LowAverage { get; set; }
    public double HighAverage { get; set; }
    public long Volume { get; set; }
}