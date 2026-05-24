using api.Data;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;

namespace api.Controllers
{
    [ApiController]
    [Route("api/Comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IScoreRepository _scoreRepository;
        public CommentController(ICommentRepository commentRepository, IScoreRepository scoreRepository)
        {
            _commentRepository = commentRepository;
            _scoreRepository = scoreRepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            var response = comments.Adapt<List<Dtos.Comment.CommentDto>>(); //Mapster for automapping 
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            var response = comment.Adapt<Dtos.Comment.CommentDto>();
            return Ok(response);
        }

        [HttpPost("{scoreId}")]
        public async Task<IActionResult> Create([FromRoute] int scoreId, [FromBody] Dtos.Comment.CommentCreateDto commentCreateDto)
        {
            if (!await _scoreRepository.ScoreExistsAsync(scoreId))
            {
                return NotFound("Score doesnt exist");
            }
            //think about passing scoreid or keep it here. if we pass it and the scope changes for comments we need to change it  
            var comment = await _commentRepository.CreateAsync(commentCreateDto.Adapt<Models.Comment>(), scoreId); //Mapster for automapping
            var response = comment.Adapt<Dtos.Comment.CommentDto>();
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Dtos.Comment.CommentUpdateDto commentUpdateDto)
        {
            var comment = await _commentRepository.UpdateAsync(commentUpdateDto.Adapt<Models.Comment>(), id);
            if (comment == null)
            {
                return NotFound();
            }
            var response = comment.Adapt<Dtos.Comment.CommentDto>();
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _commentRepository.DeleteAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}