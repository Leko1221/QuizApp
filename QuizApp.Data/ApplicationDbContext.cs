using Microsoft.EntityFrameworkCore;
using QuizApp.Core.Models;
using QuizApp.Data.ModelDb;

namespace QuizApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-one relationship between QuizResult and User
            modelBuilder.Entity<QuizResultDbModel>()
                .HasOne(qr => qr.User)
                .WithMany(u => u.QuizResults)
                .HasForeignKey(qr => qr.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-one relationship between QuizResult and Quiz
            modelBuilder.Entity<QuizResultDbModel>()
                .HasOne(qr => qr.Quiz)
                .WithMany()
                .HasForeignKey(qr => qr.QuizId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-many relationship between User and Quiz
            modelBuilder.Entity<QuizDbModel>()
                .HasOne(q => q.User)
                .WithMany(u => u.Quizzes)
                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-many relationship between Quiz and Question
            modelBuilder.Entity<QuestionDbModel>()
                .HasOne(q => q.Quiz)
                .WithMany(qz => qz.Questions)
                .HasForeignKey(q => q.QuizId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-many relationship between Question and Asnwer
            modelBuilder.Entity<AnswerDbModel>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<UserDbModel> Users { get; set; }
        public DbSet<QuizDbModel> Quizzes { get; set; }
        public DbSet<QuestionDbModel> Questions { get; set; }
        public DbSet<AnswerDbModel> Answers { get; set; }
        public DbSet<QuizResultDbModel> QuizResults { get; set; }
    }
}
