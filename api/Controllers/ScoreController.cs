using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;

namespace api.Controllers
{
    [ApiController]
    [Route("api/Score")]
    public class ScoreController : ControllerBase
    {
        //small code change for github actions setup - we need to run it for a pr once so it shows up for required checks in the future.
        private readonly ApplicationDBContext _context;
        private readonly IScoreRepository _scoreRepository;

        public ScoreController(ApplicationDBContext context, IScoreRepository scoreRepository)
        {
            _context = context;
            _scoreRepository = scoreRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetScores()
        {
            var scores = await _scoreRepository.GetAllAsync();
            var response = scores.Adapt<List<Dtos.Score.ScoreDto>>();//Mapster for automapping
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var score = await _context.Scores.FindAsync(id);
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
        public async Task<IActionResult> CreateScore([FromBody] Dtos.Score.CreateScoreRequestDto createScoreRequestDto)
        {
            var score = createScoreRequestDto.Adapt<Models.Score>();
            _context.Scores.Add(score);
            await _context.SaveChangesAsync();
            var response = score.Adapt<Dtos.Score.ScoreDto>();
            return CreatedAtAction(nameof(GetById), new { id = score.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScore([FromRoute] int id, [FromBody] Dtos.Score.UpdateScoreRequestDto updateScoreRequestDto)
        {
            var score = await _context.Scores.FindAsync(id); //abstract this
            if (score == null)
            {
                return NotFound();
            }
            score.Value = updateScoreRequestDto.Value;
            await _context.SaveChangesAsync();
            var response = score.Adapt<Dtos.Score.ScoreDto>();
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScore([FromRoute] int id)
        {
            var score = await _context.Scores.FindAsync(id);
            if (score == null)
            {
                return NotFound();
            }
            _context.Scores.Remove(score); //Remove is not async... idk why tbqh fam
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}