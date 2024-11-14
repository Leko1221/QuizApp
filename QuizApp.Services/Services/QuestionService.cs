using Microsoft.EntityFrameworkCore;
using QuizApp.Core.Models;
using QuizApp.Data.ModelDb;
using QuizApp.Data;
using QuizApp.Services.Abstractions;

namespace QuizApp.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext _context;

        public QuestionService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a new question using the Question view model
        public async Task<QuestionDbModel> CreateQuestionAsync(Question question)
        {
            if (string.IsNullOrEmpty(question.Text))
            {
                throw new ArgumentException("Question text is required.", nameof(question.Text));
            }

            // Manual mapping from Question view model to QuestionDbModel with new GUIDs for answers
            var questionDbModel = new QuestionDbModel
            {
                Id = Guid.NewGuid(),
                Text = question.Text,
                TimeLimitInSeconds = question.TimeLimitInSeconds,
                Answers = question.Answer?.Select(a => new AnswerDbModel
                {
                    Id = Guid.NewGuid(),
                    Text = a.Text,
                    IsCorrect = a.IsCorrect
                }).ToList()
            };

            _context.Questions.Add(questionDbModel);
            await _context.SaveChangesAsync();
            return questionDbModel;
        }

        // Update an existing question using the Question view model
        public async Task<QuestionDbModel> UpdateQuestionAsync(Guid questionId, Question updatedQuestion)
        {
            var existingQuestion = await _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == questionId);

            if (existingQuestion == null)
            {
                throw new KeyNotFoundException("Question not found.");
            }

            // Update fields in the existing question
            existingQuestion.Text = updatedQuestion.Text ?? existingQuestion.Text;
            existingQuestion.TimeLimitInSeconds = updatedQuestion.TimeLimitInSeconds;

            // Clear existing answers and add the updated ones with new GUIDs
            existingQuestion.Answers.Clear();
            if (updatedQuestion.Answer != null && updatedQuestion.Answer.Any())
            {
                var newAnswers = updatedQuestion.Answer.Select(a => new AnswerDbModel
                {
                    Id = Guid.NewGuid(),
                    Text = a.Text,
                    IsCorrect = a.IsCorrect
                }).ToList();
               // existingQuestion.Answers.AddRange(newAnswers);
            }

            await _context.SaveChangesAsync();
            return existingQuestion;
        }

        // Get all questions by Quiz ID
        public async Task<IEnumerable<QuestionDbModel>> GetAllQuestionsByQuizIdAsync(Guid quizId)
        {
            return await _context.Questions
                .Where(q => q.QuizId == quizId)
                .Include(q => q.Answers)
                .ToListAsync();
        }

        // Get a specific question by ID
        public async Task<QuestionDbModel> GetQuestionByIdAsync(Guid id)
        {
            return await _context.Questions
                .Where(q => q.Id == id)
                .Include(q => q.Answers)
                .FirstOrDefaultAsync();
        }

        // Delete a question by ID
        public async Task<bool> DeleteQuestionAsync(Guid questionId)
        {
            var question = await _context.Questions.FindAsync(questionId);
            if (question == null)
                return false;

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
