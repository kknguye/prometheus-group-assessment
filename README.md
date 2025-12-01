# Prometheus Group - Technical Assessment

A web application built with React and ASP.NET Core that fetches and displays daily stock summaries using the AlphaVantage API.

## Setup

### Backend

1. Navigate to the backend directory:
   ```bash
   cd backend
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Configure API key in `appsettings.json`:
   ```json
   {
     "AlphaVantage": {
       "ApiKey": "YOUR_API_KEY"
     }
   }
   ```

4. Run the backend:
   ```bash
   dotnet run
   ```

   Backend runs on `http://localhost:5210`

### Frontend

1. Navigate to the frontend directory:
   ```bash
   cd frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Run the frontend:
   ```bash
   npm run dev
   ```

   Frontend runs on `http://localhost:5173`

## Usage

1. Open `http://localhost:5173` in your browser
2. Enter a stock symbol (e.g., AAPL, MSFT)
3. Click "Search" to view daily summaries

## API Endpoint

- `GET /api/stockintraday/{symbol}` - Get daily summaries for a stock symbol

