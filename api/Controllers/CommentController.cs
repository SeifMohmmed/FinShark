using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController(
        ICommentRepository commentRepository,
        IStockRepository stockRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await commentRepository.GetAllAsync();

            var commentDto = comment.Select(c=>c.ToCommentDto());

            return Ok(commentDto);
        }

        [HttpGet("id:int")]
        public async Task<IActionResult> GetComment([FromRoute]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await commentRepository.GetById(id);

            if(comment is null)
                return NotFound();
            
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> AddComment([FromRoute] int id,CreateCommentRequestDto createComment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!await stockRepository.IsExist(id))
            {
                return BadRequest("Stock does not exist");
            }

            var commentModel = createComment.ToCommentFromCreate(id);
            await commentRepository.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetComment),new{id = commentModel.Id}, commentModel.ToCommentDto());
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