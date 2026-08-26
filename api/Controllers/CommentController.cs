using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController(
        ICommentRepository commentRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comment = await commentRepository.GetAllAsync();

            var commentDto = comment.Select(c=>c.ToCommentDto());

            return Ok(commentDto);
        }
    }
}