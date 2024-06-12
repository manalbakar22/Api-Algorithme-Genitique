namespace ApiGenitique.Models
{
    using System.ComponentModel.DataAnnotations;

    namespace ApiGenitique.Models
    {
        public class Quiz
        {
            [Key]
            public int Id { get; set; }
            public string Title { get; set; }
            public int CourseId { get; set; }
            public Course Course { get; set; }
            public float Score { get; set; }
        }
    }

}
