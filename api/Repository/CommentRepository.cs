
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository(
        ApplicationDbContext context) : ICommentRepository
    {
        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await context.Comments.AddAsync(commentModel);
            await context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await context.Comments.FindAsync(id);

            if (comment is null)
            {
                return null;
            }

            context.Comments.Remove(comment);
            await context.SaveChangesAsync();

            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await context.Comments.Include(c => c.User).ToListAsync();
        }

        public async Task<Comment?> GetById(int id)
        {
            return await context.Comments.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var comment = await context.Comments.FindAsync(id);

            if (comment is null)
            {
                return null;
            }

            comment.Title = commentModel.Title;
            comment.Content = commentModel.Content;

            context.Comments.Update(comment);
            await context.SaveChangesAsync();

            return comment;
        }
    }
}