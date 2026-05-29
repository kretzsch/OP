using api.Data;
using api.Interfaces;
using api.Models;
using api.Queries;
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

        public async Task<List<Score>> GetAllAsync(ScoreQueryObject scoreQueryObject)
        {
            //return await _context.Scores.Include(s => s.Comments).ToListAsync();
            var scoresQuery = _context.Scores.Include(s => s.Comments).AsQueryable();
            if (scoreQueryObject.PlayerId.HasValue)
            {
                scoresQuery = scoresQuery.Where(s => s.PlayerId == scoreQueryObject.PlayerId.Value);
            }
            if (scoreQueryObject.SortBy != null)
            //if (!string.IsNullOrWhiteSpace(scoreQueryObject.SortBy))
            {
                if (scoreQueryObject.SortBy.Equals("Value", StringComparison.OrdinalIgnoreCase))
                {
                    scoresQuery = scoreQueryObject.IsDescending
                        ? scoresQuery.OrderByDescending(s => s.Value)
                        : scoresQuery.OrderBy(s => s.Value);
                }
            }

            var skipNumber = (scoreQueryObject.PageNumber - 1) * scoreQueryObject.PageSize;
            scoresQuery = scoresQuery.Skip(skipNumber).Take(scoreQueryObject.PageSize);
            return await scoresQuery.ToListAsync();
        }

        public async Task<Score?> GetByIdAsync(int id)
        {
            var scoreModel = await _context.Scores.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id); //Find vinnie nie leuk, look into why 
            return scoreModel;
        }

        public Task<bool> ScoreExistsAsync(int id)
        {
            return _context.Scores.AnyAsync(s => s.Id == id);
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