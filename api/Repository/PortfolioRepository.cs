using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository(
        ApplicationDbContext context
    ) : IPortfolioRepository
    {
        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await context.Portfolios.AddAsync(portfolio);
            await context.SaveChangesAsync();

            return portfolio;
        }

        public async Task<Portfolio> DeleteAsync(ApplicationUser user, string symbol)
        {
            var portfolioModel = await context.Portfolios
                .FirstOrDefaultAsync(x => x.UserId == user.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());

            if (portfolioModel == null)
            {
                return null;
            }

            context.Portfolios.Remove(portfolioModel);
            await context.SaveChangesAsync();
            return portfolioModel;
        }

        public async Task<List<Stock>> GetUserPortfolio(ApplicationUser user)
        {
            return await context.Portfolios.Where(u => u.UserId == user.Id)
            .Select(stock => new Stock
            {
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Purchase = stock.Stock.Purchase,
                LastDiv = stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap
            }).ToListAsync();
        }
    }
}