using Microsoft.AspNetCore.Mvc;
using QuizApp.Core.Models;
using QuizApp.Services.Abstractions;

namespace QuizApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        // Create an answer (using the Answer view model)
        [HttpPost]
        public async Task<IActionResult> CreateAnswer([FromBody] Answer answer)
        {
            var createdAnswer = await _answerService.CreateAnswerAsync(answer);
            return CreatedAtAction(nameof(GetAnswerById), new { id = createdAnswer.Id }, createdAnswer);
        }

        // Update an answer (using the Answer view model)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnswer(Guid id, [FromBody] Answer updatedAnswer)
        {
            var answer = await _answerService.UpdateAnswerAsync(id, updatedAnswer);
            return Ok(answer);
        }

        // Delete an answer by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(Guid id)
        {
            await _answerService.DeleteAnswerAsync(id);
            return NoContent();
        }

        // Get all answers by Question ID
        [HttpGet("by-question/{questionId}")]
        public async Task<IActionResult> GetAllAnswersByQuestionId(Guid questionId)
        {
            var answers = await _answerService.GetAllAnswersByQuestionIdAsync(questionId);
            return Ok(answers);
        }

        // Get a specific answer by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnswerById(Guid id)
        {
            var answer = await _answerService.GetAnswerByIdAsync(id);
            if (answer == null)
            {
                return NotFound();
            }
            return Ok(answer);
        }
    }
}
