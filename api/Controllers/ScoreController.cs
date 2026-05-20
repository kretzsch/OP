using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Models;
using Mapster;
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
            var response = scores.Adapt<List<Dtos.Score.ScoreDto>>();//Mapster for automapping
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var score = _context.Scores.Find(id);
            if (score == null)
            {
                return NotFound();
            }
            var response = score.Adapt<Dtos.Score.ScoreDto>(); //Maspster for automapping
            return Ok(response);
        }


        /* 
        for now you cant log in, so we just pass the playerid for now. 
        but when we have login it needs to be 

        psuedocode:

        [authorize]
        
        playeridclaim = user.findfirst (claimtype playerid)

        */
        [HttpPost]
        public IActionResult CreateScore([FromBody] Dtos.Score.CreateScoreRequestDto createScoreRequestDto)
        {
            var score = createScoreRequestDto.Adapt<Models.Score>();
            _context.Scores.Add(score);
            _context.SaveChanges();
            var response = score.Adapt<Dtos.Score.ScoreDto>();
            return CreatedAtAction(nameof(GetById), new { id = score.Id }, response);
        }
    }
}