using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController(ApplicationDbContext context) : ControllerBase
    {
        
        [HttpGet]
        public async Task <IActionResult> GetStocks()
        {
            var stocks =  await context.Stocks.ToListAsync();

            var stockDto = stocks .Select(s=>s.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetStock(int id)
        {
            var stock = await context.Stocks.FindAsync(id);

            if (stock is null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto createStockRequestDto)
        {
            var stock = createStockRequestDto.ToStockFromCreateStockDto();
            await context.Stocks.AddAsync(stock);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stock.ToStockDto());
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
        {
            var stock = await context.Stocks.FindAsync(id);

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

            await context.SaveChangesAsync();

            return Ok(stock.ToStockDto());
        }
    
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if (stock is null)
            {
                return NotFound();
            }

            context.Stocks.Remove(stock);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}