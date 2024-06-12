using ApiGenitique.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Ajoutez les services au conteneur.
builder.Services.AddControllers();

// Configurez la connexion � la base de donn�es
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Enregistrez le service GeneticAlgorithmService avec une dur�e de vie transitoire
builder.Services.AddTransient<GeneticAlgorithmService>();

// Enregistrez le service ILogger<GeneticAlgorithmService> pour l'injection dans le constructeur
builder.Services.AddTransient<ILogger<GeneticAlgorithmService>, Logger<GeneticAlgorithmService>>();

// Configurez Swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurez le pipeline de requ�te HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();