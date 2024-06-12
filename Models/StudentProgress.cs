namespace ApiGenitique.Models
{
    using global::ApiGenitique.Models.ApiGenitique.Models;
    using System.ComponentModel.DataAnnotations;

    namespace ApiGenitiqueA.Models
    {
        public class StudentProgress
        {
            [Key]
            public int Id { get; set; }
            public int StudentId { get; set; }
            public Student Student { get; set; }
            public int CourseId { get; set; }
            public Course Course { get; set; }
            public int QuizId { get; set; }
            public Quiz Quiz { get; set; }
            public bool IsCompleted { get; set; }
            public float Score { get; set; }
        }
    }

}
