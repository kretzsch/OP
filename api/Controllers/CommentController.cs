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
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
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
    }
}