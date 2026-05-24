using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            var commentModel = await _context.Comments.FindAsync(id);
            return commentModel;
        }

        public async Task<Comment> CreateAsync(Comment comment, int scoreId)
        {
            comment.ScoreId = scoreId;
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
        public async Task<Comment?> UpdateAsync(Comment comment, int id)
        {
            var commentModel = await _context.Comments.FindAsync(id);
            if (commentModel == null)
            {
                return null;
            }
            commentModel.Title = comment.Title;
            commentModel.Content = comment.Content;
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentModel = await _context.Comments.FindAsync(id);
            if (commentModel == null)
            {
                return null;
            }
            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }
    }
}