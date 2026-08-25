using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController(ApplicationDbContext context) : ControllerBase
    {
        
        [HttpGet]
        public  IActionResult GetStocks()
        {
            var stocks =  context.Stocks.ToList();
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public  IActionResult GetStock(int id)
        {
            var stock =  context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }
    }
}