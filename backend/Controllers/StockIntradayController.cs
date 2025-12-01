using Microsoft.AspNetCore.Mvc;
using Backend.Services;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockIntradayController(StockIntradayService stockIntradayService) : ControllerBase
{
    /// <summary>
    /// Gets daily summaries of intraday stock data for the specified symbol.
    /// </summary>
    /// <param name="symbol">The stock symbol</param>
    /// <returns>Returns 200 OK with daily stock summaries</returns>
    [HttpGet("{symbol}")]
    public async Task<IActionResult> GetIntradaySummary(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
        {
            return BadRequest(new { error = "Stock symbol cannot be empty. Please provide a valid stock symbol (e.g., AAPL, MSFT, GOOGL)" });
        }

        return Ok(await stockIntradayService.GetIntradaySummaryAsync(symbol.ToUpper()));
    }
}