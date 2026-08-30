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
    [Route("portfolio")]
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
    }
}