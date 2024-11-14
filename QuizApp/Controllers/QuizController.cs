using Microsoft.AspNetCore.Mvc;
using QuizApp.Core.Models;
using QuizApp.Data.ModelDb;
using QuizApp.Services.Abstractions;

namespace QuizApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuiz([FromBody] Quiz quiz)
        {
            try
            {
                var createdQuiz = await _quizService.CreateQuizAsync(quiz);
                return CreatedAtAction(nameof(GetQuizById), new { quizId = createdQuiz.Id }, createdQuiz);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDbModel>>> GetAllQuizzes()
        {
            var quizzes = await _quizService.GetAllQuizzesAsync();
            return Ok(quizzes);
        }

        [HttpGet("{quizId}")]
        public async Task<ActionResult<QuizDbModel>> GetQuizById(Guid quizId)
        {
            try
            {
                var quiz = await _quizService.GetQuizByIdAsync(quizId);
                return Ok(quiz);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(Guid id)
        {
            var result = await _quizService.DeleteQuizAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
