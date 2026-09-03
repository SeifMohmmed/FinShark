using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("/api/portfolio")]
    public class PortfolioController(
        UserManager<ApplicationUser> userManager,
        IFMPService fmpService,
        IPortfolioRepository portfolioRepository,
        IStockRepository stockRepository
    ) : ControllerBase
    {

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPortfolio()
        {
            var userName = User.GetUsername();
            var user = await userManager.FindByNameAsync(userName);
            var userPortfolio = await portfolioRepository.GetUserPortfolio(user);

            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToPortfolio(string symbol)
        {
            var userName = User.GetUsername();
            var user = await userManager.FindByNameAsync(userName);

            if (user is null)
            {
                return Unauthorized("User not found");
            }

            var stock = await stockRepository.GetBySymbolAsync(symbol);

            if (stock is null)
            {
                stock = await fmpService.FindStockBySymbolAsync(symbol);

                if (stock is null)
                {
                    return NotFound("Stock not found");
                }

                await stockRepository.CreateAsync(stock);

            }

            var portfolio = new Portfolio
            {
                UserId = user.Id,
                StockId = stock.Id
            };

            await portfolioRepository.CreateAsync(portfolio);

            return NoContent();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> RemoveFromPortfolio(string symbol)
        {
            var userName = User.GetUsername();
            var user = await userManager.FindByNameAsync(userName);

            var userPortfolio = await portfolioRepository.GetUserPortfolio(user);

            var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

            if (filteredStock.Count() == 1)
            {
                await portfolioRepository.DeleteAsync(user, symbol);
            }

            else
            {
                return BadRequest("Stock not found in portfolio");
            }

            return NoContent();
        }
    }
}