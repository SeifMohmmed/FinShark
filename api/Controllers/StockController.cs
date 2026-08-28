using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController(
        ApplicationDbContext context,
        IStockRepository stockRepository) : ControllerBase
    {
        
        [HttpGet]
        public async Task <IActionResult> GetStocks()
        {
            var stocks =  await stockRepository.GetAllAsync();

            var stockDto = stocks .Select(s=>s.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetStock(
            int id)
        {
            var stock = await stockRepository.GetByIdAsync(id);

            if (stock is null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock(
            [FromBody] CreateStockRequestDto createStockRequestDto)
        {
            var stockModel = createStockRequestDto.ToStockFromCreateStockDto();
            await stockRepository.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetStock), new { id = stockModel.Id }, stockModel.ToStockDto());
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(
            int id,
            [FromBody] UpdateStockRequestDto updateStockRequestDto)
        {
            var stock = await stockRepository.UpdateAsync(id,updateStockRequestDto);

            if (stock is null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }
    
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await stockRepository.DeleteAsync(id);

            if (stock is null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}