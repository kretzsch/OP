using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Score;

namespace api.Interfaces
{
    public interface IScoreRepository
    {
        Task<List<Models.Score>> GetAllAsync();

        Task<Models.Score?> GetByIdAsync(int id); // ?  because it can return null if not found
        Task<Models.Score> CreateAsync(Models.Score score);
        Task<Models.Score?> UpdateAsync(int id, int scoreValue);
        Task<Models.Score?> DeleteAsync(int id);
        Task<bool> ScoreExistsAsync(int id);
    }
}