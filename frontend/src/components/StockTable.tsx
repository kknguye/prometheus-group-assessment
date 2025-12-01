import { useState, useEffect } from 'react';
import type { DailyStockSummary } from '../types/DailyStockSummary';
import { fetchStockData } from '../api/stockApi';

interface StockTableProps {
  symbol: string;
}

export function StockTable({ symbol }: StockTableProps) {
  const [data, setData] = useState<DailyStockSummary[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  // Fetch data when symbol changes
  useEffect(() => {
    if (!symbol.trim()) {
      setData([]);
      setError(null);
      return;
    }

    setLoading(true);
    setError(null);
    
    fetchStockData(symbol)
      .then(setData)
      .catch((err) => {
        setError(err instanceof Error ? err.message : 'Failed to load data');
        setData([]);
      })
      .finally(() => setLoading(false));
  }, [symbol]);

  if (!symbol.trim()) {
    return null;
  }

  if (loading) {
    return <div className="loading">Loading data...</div>;
  }

  if (error) {
    return <div className="error">{error}</div>;
  }

  if (data.length === 0) {
    return null;
  }

  return (
    <div className="results">
      <h2>Daily Summaries for {symbol}</h2>
      <table>
        <thead>
          <tr>
            <th>Date</th>
            <th>Average Low</th>
            <th>Average High</th>
            <th>Volume</th>
          </tr>
        </thead>
        <tbody>
          {data.map((item, index) => (
            <tr key={index}>
              <td>{item.day}</td>
              <td>${item.lowAverage}</td>
              <td>${item.highAverage}</td>
              <td>{item.volume.toLocaleString()}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

