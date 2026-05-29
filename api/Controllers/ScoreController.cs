using api.Data;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Queries;

namespace api.Controllers
{
    [ApiController]
    [Route("api/Score")]
    public class ScoreController : ControllerBase
    {
        private readonly IScoreRepository _scoreRepository;

        public ScoreController(IScoreRepository scoreRepository)
        {
            _scoreRepository = scoreRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetScores([FromQuery] ScoreQueryObject scoreQueryObject)
        {
            var scores = await _scoreRepository.GetAllAsync(scoreQueryObject);
            var response = scores.Adapt<List<Dtos.Score.ScoreDto>>();//Mapster for automapping
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        { //Modelstate not needed 
            var score = await _scoreRepository.GetByIdAsync(id);
            if (score == null)
            {
                return NotFound();
            }
            var response = score.Adapt<Dtos.Score.ScoreDto>(); //Mapster for automapping 
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
            var score = await _scoreRepository.CreateAsync(createScoreRequestDto.Adapt<Models.Score>()); //Mapster for automapping
            var response = score.Adapt<Dtos.Score.ScoreDto>();
            return CreatedAtAction(nameof(GetById), new { id = score.Id }, response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateScore([FromRoute] int id, [FromBody] Dtos.Score.UpdateScoreRequestDto updateScoreRequestDto)
        { //modelstate not needed because of the [ApiController] attribute, it automatically checks the model state and returns a 400 if it's invalid
            var score = await _scoreRepository.UpdateAsync(id, updateScoreRequestDto.Value);
            if (score == null)
            {
                return NotFound();
            }
            var response = score.Adapt<Dtos.Score.ScoreDto>();
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteScore([FromRoute] int id)
        {
            var score = await _scoreRepository.DeleteAsync(id);
            if (score == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}