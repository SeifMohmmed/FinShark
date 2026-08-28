using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>>GetAllAsync();
        Task<Comment?> GetById(int id);
        Task<Comment> CreateAsync(Comment commentModel);
    }
}