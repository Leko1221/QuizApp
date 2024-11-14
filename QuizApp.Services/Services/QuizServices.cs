using QuizApp.Core.Models;
using QuizApp.Data;
using QuizApp.Data.Abstractions;
using QuizApp.Data.ModelDb;
using QuizApp.Services.Abstractions;

namespace QuizApp.Services.Services
{
    public class QuizService : IQuizService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IQuizRepository _quizRepository;

        public QuizService(ApplicationDbContext context,
                           IUserRepository userRepository,
                           IQuizRepository quizRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _quizRepository = quizRepository;
        }
     
        public async Task<QuizDbModel> CreateQuizAsync(Quiz quiz)
        {
            if (quiz.Questions.Count > 20)
                throw new ArgumentException("A quiz can have a maximum of 20 questions.");

            var user = await _userRepository.GetUserWithIncludesIdAsync(quiz.User.Id);

            if (user == null)
                throw new InvalidOperationException($"User with ID {quiz.User.Id} does not exist.");

            var quizDbModel = new QuizDbModel
            {
                Id = Guid.NewGuid(),
                Title = quiz.Title,
                UserId = user.Id,
                QuestionId = Guid.NewGuid(),
                User = user,
                CreatedAt = quiz.CreatedAt,
                Questions = new List<QuestionDbModel>()
            };

            foreach (var question in quiz.Questions)
            {
                if (question.Answer.Count != 4)
                    throw new ArgumentException("Each question must have exactly 4 answers.");

                if (question.Answer.Count(a => a.IsCorrect == true) != 1)
                    throw new ArgumentException("Each question must have exactly one correct answer.");

                var questionDbModel = new QuestionDbModel
                {
                    Id = quizDbModel.QuestionId,
                    Text = question.Text,
                    TimeLimitInSeconds = 10,  
                    QuizId = quizDbModel.Id,
                    Answers = new List<AnswerDbModel>()
                };

                foreach (var answer in question.Answer)
                {
                    var answerDbModel = new AnswerDbModel
                    {
                        Id = Guid.NewGuid(),
                        Text = answer.Text,
                        IsCorrect = answer.IsCorrect,
                        QuestionId = questionDbModel.Id,
                    };

                    questionDbModel.Answers.Add(answerDbModel);
                }

                quizDbModel.Questions.Add(questionDbModel);
            }

            _context.Quizzes.Add(quizDbModel);
            await _context.SaveChangesAsync();
            return quizDbModel;
        }

        public async Task<List<Quiz>> GetAllQuizzesAsync()
        {
            var quizzes = await _quizRepository.GetAllQuizzesWithIncludesAsync();
            var result = new List<Quiz>();

            foreach (var quiz in quizzes)
            {
                var quizModel = new Quiz
                {
                    Id = quiz.Id,
                    Title = quiz.Title,
                    CreatedAt = DateTime.Now,
                };

                var user = await _userRepository.GetByIdAsync(quiz.UserId);
                if (user != null)
                {
                    quizModel.User = new User
                    {
                        Id = user.Id,
                        DisplayName = user.DisplayName,
                        Email = user.Email,
                        PasswordHash = user.PasswordHash
                    };
                }

                if (quiz.Questions != null)
                {
                    foreach (var question in quiz.Questions)
                    {
                        var questionModel = new Question
                        {
                            Id = question.Id,
                            Text = question.Text,
                            TimeLimitInSeconds = question.TimeLimitInSeconds
                        };

                        List<Question> questions = new List<Question>();
                        List<Answer> answerList = new List<Answer>();

                        foreach (var answer in question.Answers)
                        {
                            var answerModel = new Answer
                            {
                                Id = answer.Id,
                                IsCorrect = answer.IsCorrect,
                                Text = answer.Text
                            };

                            answerList.Add(answerModel);
                        }

                        questions.Add(questionModel);
                        questionModel.Answer = answerList;
                        quizModel.Questions = questions;
                    }
                }
                result.Add(quizModel);
            }
            return result;
        }

        public async Task<Quiz> GetQuizByIdAsync(Guid? quizId)
        {
            var quiz = await _quizRepository.GetQuizWithIncludesIdAsync(quizId);

            if (quiz == null)
            {
                throw new KeyNotFoundException("Quiz not found.");
            }

            var quizModel = new Quiz 
            { Id = quizId, 
              Title = quiz.Title, 
              CreatedAt = DateTime.Now,
              User = new User { Id = quiz.User.Id, DisplayName = quiz.User.DisplayName, Email = quiz.User.Email, PasswordHash = quiz.User.PasswordHash }
            };

            if (quiz.Questions != null)
            {
                foreach( var question in quiz.Questions)
                {
                    var questionModel = new Question
                    {
                        Id = question.Id,
                        Text = question.Text,
                        TimeLimitInSeconds = question.TimeLimitInSeconds
                    };

                    List<Question> questions = new List<Question>();
                    List<Answer> answerList = new List<Answer>();

                    foreach (var answer in question.Answers)
                    {
                        var answerModel = new Answer
                        {
                            Id = answer.Id,
                            IsCorrect = answer.IsCorrect,
                            Text = answer.Text
                        };

                        answerList.Add(answerModel);
                    }

                    questions.Add(questionModel);
                    questionModel.Answer = answerList;
                    quizModel.Questions = questions;
                }
            }
            return quizModel;
        }

        public async Task<bool> DeleteQuizAsync(Guid quizId)
        {
            var quiz = await _context.Quizzes.FindAsync(quizId);
            if (quiz == null) throw new KeyNotFoundException("Quiz not found.");

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
