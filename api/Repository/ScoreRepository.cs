using api.Data;
using api.Dtos.Score;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly ApplicationDBContext _context;

        public ScoreRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Score> CreateAsync(Score scoreModel)
        {
            _context.Scores.Add(scoreModel);
            await _context.SaveChangesAsync();
            return scoreModel;
        }

        public async Task<Score?> DeleteAsync(int id)
        {
            var scoreModel = await _context.Scores.FindAsync(id);
            if (scoreModel == null)
            {
                return null;
            }
            _context.Scores.Remove(scoreModel); //Remove is not async... idk why tbqh fam
            await _context.SaveChangesAsync();
            return scoreModel;
        }

        public async Task<List<Score>> GetAllAsync()
        {
            return await _context.Scores.ToListAsync();
        }

        public async Task<Score?> GetByIdAsync(int id)
        {
            var scoreModel = await _context.Scores.FindAsync(id);
            return scoreModel;
        }

        //we dont need the whole dto here, just the score value, so we can just pass that instead of the whole dto.
        public async Task<Score?> UpdateAsync(int id, int scoreValue)
        {
            var scoreModel = await _context.Scores.FindAsync(id);
            if (scoreModel == null)
            {
                return null;
            }
            scoreModel.Value = scoreValue;
            await _context.SaveChangesAsync();
            return scoreModel;
        }
    }
}