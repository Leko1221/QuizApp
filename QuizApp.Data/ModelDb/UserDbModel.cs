using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Data.ModelDb
{
    public class UserDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? Id { get; set; }
        [MaxLength(150)]
        [Required]
        public string Email { get; set; }
        [MaxLength(150)]
        [Required]
        public string PasswordHash { get; set; }
        [MaxLength(150)]
        [Required]
        public string DisplayName { get; set; }

        [JsonIgnore] // Ignore the Quizzes collection during serialization
        public ICollection<QuizDbModel>? Quizzes { get; set; }
        [JsonIgnore] // Ignore the QuizResults collection during serialization
        public ICollection<QuizResultDbModel>? QuizResults { get; set; }
    }
}
