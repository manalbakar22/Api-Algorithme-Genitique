using ApiGenitique.Models;
using ApiGenitique.Models.ApiGenitique.Models;
using ApiGenitique.Models.ApiGenitiqueA.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentProgress> StudentProgresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuration des entités existantes
        modelBuilder.Entity<Course>()
            .HasOne(c => c.Category)
            .WithMany(c => c.Courses)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Category>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .IsRequired();

        modelBuilder.Entity<StudentProgress>()
            .HasOne(sp => sp.Course)
            .WithMany()
            .HasForeignKey(sp => sp.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StudentProgress>()
            .HasOne(sp => sp.Quiz)
            .WithMany()
            .HasForeignKey(sp => sp.QuizId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StudentProgress>()
            .HasOne(sp => sp.Student)
            .WithMany()
            .HasForeignKey(sp => sp.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Ajouter des données de test pour les catégories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Awareness", Code = "A" },
            new Category { Id = 2, Name = "Exposure", Code = "E" },
            new Category { Id = 3, Name = "Appropriation", Code = "AP" },
            new Category { Id = 4, Name = "Assimilation", Code = "AS" },
            new Category { Id = 5, Name = "Development", Code = "D" }
        );

        // Ajouter des données de test pour les cours en spécifiant seulement les clés étrangères
        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, Title = "Course A1", CategoryId = 1, CompletionRate = 0.9f },
            new Course { Id = 2, Title = "Course E1", CategoryId = 2, CompletionRate = 0.85f },
            new Course { Id = 3, Title = "Course AP1", CategoryId = 3, CompletionRate = 0.75f },
            new Course { Id = 4, Title = "Course AS1", CategoryId = 4, CompletionRate = 0.95f },
            new Course { Id = 5, Title = "Course D1", CategoryId = 5, CompletionRate = 0.8f }
        );

        // Ajouter des données de test pour les quizzes en spécifiant seulement les clés étrangères
        modelBuilder.Entity<Quiz>().HasData(
            new Quiz { Id = 1, Title = "Quiz 1", CourseId = 1, Score = 0.75f },
            new Quiz { Id = 2, Title = "Quiz 2", CourseId = 1, Score = 0.9f },
            new Quiz { Id = 3, Title = "Quiz 3", CourseId = 2, Score = 0.6f },
            new Quiz { Id = 4, Title = "Quiz 4", CourseId = 3, Score = 0.85f },
            new Quiz { Id = 5, Title = "Quiz 5", CourseId = 4, Score = 0.7f }
        );
    }
}
