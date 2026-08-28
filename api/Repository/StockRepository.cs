using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository(ApplicationDbContext context) : IStockRepository
    {
        public async Task<Stock> CreateAsync(Stock stock)
        {
            await context.Stocks.AddAsync(stock);
            await context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await context.Stocks.FirstOrDefaultAsync(s=>s.Id==id);
            
            if(stockModel is null)
                return null;
            
            context.Stocks.Remove(stockModel);
            await context.SaveChangesAsync();
            
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
          return  await context.Stocks.Include(c=>c.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await context.Stocks.Include(c=>c.Comments).FirstOrDefaultAsync(s=>s.Id==id);
        }

        public Task<bool> IsExist(int id)
        {
            return context.Stocks.AnyAsync(s=>s.Id==id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockRequestDto)
        {
           var stockModel = await context.Stocks.FirstOrDefaultAsync(s=>s.Id==id);
        
            if(stockModel is null)
                return null;

            context.Entry(stockModel).CurrentValues.SetValues(stockRequestDto);
            await context.SaveChangesAsync();
            
            return stockModel;
        }
    }
}