using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Newtonsoft.Json;

namespace api.Services
{
    public class FMPService(
        HttpClient httpClient,
        IConfiguration configuration) : IFMPService
    {
        public async Task<Stock> FindStockBySymbolAsync(string symbol)
        {
            try
            {
                var result = await httpClient
                .GetAsync($"https://financialmodelingprep.com/stable/profile?symbol={symbol}&apikey={configuration["FMPKey"]}");

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var stocks = JsonConvert.DeserializeObject<FMPStock[]>(content);

                    if (stocks == null || stocks.Length == 0)
                    {
                        return null;
                    }

                    var stock = stocks[0];

                    return stock.ToStockFromFMP();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching stock data for symbol {symbol}: {ex.Message}", ex);
            }
        }
    }
}