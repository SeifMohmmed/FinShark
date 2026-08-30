using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        IPortfolioRepository portfolioRepository,
        IStockRepository stockRepository
    ) : ControllerBase
    {

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPortfolio()
        {
            var userId = User.GetUsername();
            var user = await userManager.FindByNameAsync(userId);
            var userPortfolio = await portfolioRepository.GetUserPortfolio(user);

            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToPortfolio(string symbol)
        {
            var userId = User.GetUsername();
            var user = await userManager.FindByNameAsync(userId);

            var stock = await stockRepository.GetBySymbolAsync(symbol);

            if (stock is null)
                return NotFound("Stock not found");

            var portfolio = new Portfolio
            {
                UserId = user.Id,
                StockId = stock.Id
            };

            await portfolioRepository.CreateAsync(portfolio);


            if (portfolio is null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }
    }
}