using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentsAsync();
        Task<Comment?> GetCommentByIdAsync(int id);
        Task<Comment> CreateAsync(Models.Comment comment, int scoreId);
        Task<Comment?> UpdateAsync(Models.Comment comment, int id);
        Task<Comment?> DeleteAsync(int id);
    }
}