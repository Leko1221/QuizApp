using QuizApp.Core.Models;
using QuizApp.Data.ModelDb;
using QuizApp.Data;
using QuizApp.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace QuizApp.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly ApplicationDbContext _context;

        public AnswerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AnswerDbModel> CreateAnswerAsync(Answer answer)
        {
            // Check if the QuestionId exists in the Questions table
            //if (answer.QuestionId == null || !await _context.Questions.AnyAsync(q => q.Id == answer.QuestionId))
            //{
               // throw new KeyNotFoundException("The specified QuestionId does not exist in the Questions table.");
            //}

            // Manual mapping from Answer view model to AnswerDbModel
            var answerDbModel = new AnswerDbModel
            {
                Id = Guid.NewGuid(),
                Text = answer.Text,
                IsCorrect = answer.IsCorrect,
                //QuestionId = answer.QuestionId.Value // Assumes QuestionId is a nullable Guid
            };

            _context.Answers.Add(answerDbModel);
            await _context.SaveChangesAsync();
            return answerDbModel; // Returning DbModel to persist the ID and other properties.
        }

        // Update an existing answer using the Answer view model
        public async Task<AnswerDbModel> UpdateAnswerAsync(Guid answerId, Answer updatedAnswer)
        {
            var existingAnswer = await _context.Answers.FindAsync(answerId);
            if (existingAnswer == null) throw new KeyNotFoundException("Answer not found.");

            // Manual mapping from Answer view model to AnswerDbModel (for update operation)
            existingAnswer.Text = updatedAnswer.Text;
            existingAnswer.IsCorrect = updatedAnswer.IsCorrect;

            await _context.SaveChangesAsync();
            return existingAnswer;
        }

        // Delete an answer by its ID
        public async Task DeleteAnswerAsync(Guid answerId)
        {
            var answer = await _context.Answers.FindAsync(answerId);
            if (answer == null) throw new KeyNotFoundException("Answer not found.");

            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
        }

        // Get all answers by Question ID (returns AnswerDbModel)
        public async Task<IEnumerable<AnswerDbModel>> GetAllAnswersByQuestionIdAsync(Guid questionId)
        {
            return await _context.Answers.Where(a => a.QuestionId == questionId).ToListAsync();
        }

        // Get a specific answer by ID (returns AnswerDbModel)
        public async Task<AnswerDbModel> GetAnswerByIdAsync(Guid id)
        {
            return await _context.Answers.FindAsync(id);
        }
    }
}
