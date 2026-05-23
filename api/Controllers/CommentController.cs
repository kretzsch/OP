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
    }
}