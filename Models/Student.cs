﻿namespace ApiGenitique.Models
{
    using System.ComponentModel.DataAnnotations;

    namespace ApiGenitique.Models
    {
        public class Student
        {
            [Key]
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }
    }

}
