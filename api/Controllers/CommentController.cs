using api.Dtos.Comment;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController(
        UserManager<ApplicationUser> userManager,
        IFMPService fmpService,
        ICommentRepository commentRepository,
        IStockRepository stockRepository) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetComments([FromQuery] CommentQueryObject queryObject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await commentRepository.GetAllAsync(queryObject);

            var commentDto = comment.Select(c => c.ToCommentDto());

            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await commentRepository.GetById(id);

            if (comment is null)
                return NotFound();

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{symbol:alpha}")]
        public async Task<IActionResult> AddComment([FromRoute] string symbol, CreateCommentRequestDto createComment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await stockRepository.GetBySymbolAsync(symbol);

            if (stock is null)
            {
                stock = await fmpService.FindStockBySymbolAsync(symbol);

                if (stock is null)
                {
                    return NotFound("Stock not found");
                }
                else
                {
                    await stockRepository.CreateAsync(stock);
                }
            }

            var userName = User.GetUsername();
            var user = await userManager.FindByNameAsync(userName);

            var commentModel = createComment.ToCommentFromCreate(stock.Id);
            commentModel.UserId = user.Id;

            await commentRepository.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetComment), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, UpdateCommentDto updateComment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await commentRepository.UpdateAsync(id, updateComment.ToCommentFromUpdate());

            if (comment is null)
            {
                return NotFound("Comment not found");
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await commentRepository.DeleteAsync(id);

            if (comment is null)
            {
                return NotFound("Comment not found");
            }

            return NoContent();
        }
    }
}