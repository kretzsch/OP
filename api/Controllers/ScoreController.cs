using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/Score")]
    public class ScoreController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ScoreController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetScores()
        {
            var scores = _context.Scores.ToList();
            return Ok(scores);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var score = _context.Scores.Find(id);
            if (score == null)
            {
                return NotFound();
            }
            return Ok(score);
        }
    }
}