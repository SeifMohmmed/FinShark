using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController(
        IStockRepository stockRepository) : ControllerBase
    {

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetStocks([FromQuery] QueryObject queryObject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await stockRepository.GetAllAsync(queryObject);

            var stockDto = stocks.Select(s => s.ToStockDto()).ToList();

            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStock(
            int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = createStockRequestDto.ToStockFromCreateStockDto();
            await stockRepository.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetStock), new { id = stockModel.Id }, stockModel.ToStockDto());

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStock(
            int id,
            [FromBody] UpdateStockRequestDto updateStockRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await stockRepository.UpdateAsync(id, updateStockRequestDto);

            if (stock is null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await stockRepository.DeleteAsync(id);

            if (stock is null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}