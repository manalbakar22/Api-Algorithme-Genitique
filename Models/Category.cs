﻿namespace ApiGenitique.Models
{
    using System.ComponentModel.DataAnnotations;



    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}

