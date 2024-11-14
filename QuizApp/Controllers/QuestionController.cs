using Microsoft.AspNetCore.Mvc;
using QuizApp.Core.Models;
using QuizApp.Services.Abstractions;
using System;

namespace QuizApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        // Create a question (using the Question view model)
        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] Question question)
        {
            if (question == null)
            {
                return BadRequest("Invalid question data.");
            }

            try
            {
                var createdQuestion = await _questionService.CreateQuestionAsync(question);
                return CreatedAtAction(nameof(GetQuestionById), new { id = createdQuestion.Id }, createdQuestion);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // Update a question (using the Question view model)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(Guid id, [FromBody] Question updatedQuestion)
        {
            if (updatedQuestion == null)
            {
                return BadRequest("Invalid question data.");
            }

            try
            {
                var question = await _questionService.UpdateQuestionAsync(id, updatedQuestion);
                return Ok(question);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // Get all questions by Quiz ID
        [HttpGet("by-quiz/{quizId}")]
        public async Task<IActionResult> GetAllQuestionsByQuizId(Guid quizId)
        {
            var questions = await _questionService.GetAllQuestionsByQuizIdAsync(quizId);
            return Ok(questions);
        }

        // Get a specific question by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(Guid id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }

        // Delete a question by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            var result = await _questionService.DeleteQuestionAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent(); // Return 204 No Content if the deletion was successful
        }
    }
}
