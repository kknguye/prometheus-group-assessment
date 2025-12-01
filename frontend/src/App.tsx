import { useState } from 'react';
import { StockSearchForm } from './components/StockSearchForm';
import { StockTable } from './components/StockTable';
import './App.css';

const App = () => {
  // Shared state between search form and table
  const [symbol, setSymbol] = useState('');

  return (
    <div className="app">
      <h1>Stock Intraday Data</h1>
      <h3>Search for a stock symbol to view daily summaries</h3>
      <StockSearchForm onSymbolChange={setSymbol} />
      <StockTable symbol={symbol} />
    </div>
  );
};

export default App;
