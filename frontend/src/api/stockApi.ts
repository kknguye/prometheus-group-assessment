import type { DailyStockSummary } from "../types/DailyStockSummary";

const API_URL = "http://localhost:5210/api/stockintraday";

export async function fetchStockData(symbol: string): Promise<DailyStockSummary[]> {
  const response = await fetch(`${API_URL}/${symbol.toUpperCase()}`);
  
  if (!response.ok) {
    // Try to extract error message from response, fallback to status code
    const error = await response.json().catch(() => ({}));
    throw new Error(error.error || `Failed to fetch data: ${response.status}`);
  }

  return await response.json();
}
