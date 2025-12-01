import { useState } from 'react';

interface StockSearchFormProps {
  onSymbolChange: (symbol: string) => void;
}

export function StockSearchForm({ onSymbolChange }: StockSearchFormProps) {
  const [symbol, setSymbol] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    // Convert to uppercase for consistency with API
    if (symbol.trim()) {
      onSymbolChange(symbol.trim().toUpperCase());
    }
  };

  return (
    <form onSubmit={handleSubmit} className="search-form">
      <input
        type="text"
        value={symbol}
        onChange={(e) => setSymbol(e.target.value.toUpperCase())}
        placeholder="Enter stock symbol (e.g., AAPL, MSFT)"
      />
      <button type="submit">Search</button>
    </form>
  );
}