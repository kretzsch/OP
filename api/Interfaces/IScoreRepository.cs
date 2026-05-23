using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IScoreRepository
    {
        Task<List<Models.Score>> GetAllAsync();

        // Task<Models.Score> GetAsync(int id);
    }
}