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
                    var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);
                    var stock = tasks[0];
                    if (stock != null)
                    {
                        return stock.ToStockFromFMP();
                    }
                    return null;
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