namespace ApiGenitique.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public float CompletionRate { get; set; }
    }
}
