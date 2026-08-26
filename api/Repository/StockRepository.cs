using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository(ApplicationDbContext context) : IStockRepository
    {
        public async Task<List<Stock>> GetAll()
        {
          return  await context.Stocks.ToListAsync();
        }
    }
}