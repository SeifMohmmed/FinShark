using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
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
            var stocks =  context.Stocks.ToList()
                .Select(s=>s.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public  IActionResult GetStock(int id)
        {
            var stock =  context.Stocks.Find(id);
            if (stock is null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult CreateStock([FromBody] CreateStockRequestDto createStockRequestDto)
        {
            var stock = createStockRequestDto.ToStockFromCreateStockDto();
            context.Stocks.Add(stock);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stock.ToStockDto());
            
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStock(int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
        {
            var stock = context.Stocks.Find(id);
            if (stock is null)
            {
                return NotFound();
            }

            //#1
            context.Entry(stock).CurrentValues.SetValues(updateStockRequestDto);

            // #2
            // stock.Symbol = updateStockRequestDto.Symbol;
            // stock.CompanyName = updateStockRequestDto.CompanyName;
            // stock.Purchase = updateStockRequestDto.Purchase;
            // stock.LastDiv = updateStockRequestDto.LastDiv;
            // stock.Industry = updateStockRequestDto.Industry;
            // stock.MarketCap = updateStockRequestDto.MarketCap;

            context.SaveChanges();

            return Ok(stock.ToStockDto());
        }
    
        [HttpDelete("{id}")]
        public IActionResult DeleteStock(int id)
        {
            var stock = context.Stocks.FirstOrDefault(s => s.Id == id);
            if (stock is null)
            {
                return NotFound();
            }

            context.Stocks.Remove(stock);
            context.SaveChanges();

            return NoContent();
        }
    }
}