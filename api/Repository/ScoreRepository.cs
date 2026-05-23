using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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

        public Task<List<Score>> GetAllAsync()
        {
            return _context.Scores.ToListAsync();
        }

        /*public Task<Score> GetAsync(int id)
        {
           return _context.Scores.FindAsync(id);
        } */
    }
}