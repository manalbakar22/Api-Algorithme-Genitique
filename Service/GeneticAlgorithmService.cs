using System;
using System.Collections.Generic;
using System.Linq;
using ApiGenitique.Models;
using Microsoft.Extensions.Logging;

namespace ApiGenitique.Service
{
    public class GeneticAlgorithmService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GeneticAlgorithmService> _logger;
        private static readonly Random random = new Random();

        public GeneticAlgorithmService(ApplicationDbContext context, ILogger<GeneticAlgorithmService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<string> GenerateSequence(int length, int maxQuizPerCategory)
        {
            _logger.LogInformation("Starting sequence generation...");
            var categories = _context.Categories.ToList();
            var sequence = new List<string>();

            try
            {
                for (int i = 0; i < length; i++)
                {
                    var category = categories[random.Next(categories.Count)];
                    var quizCount = random.Next(0, maxQuizPerCategory + 1);

                    for (int j = 0; j < quizCount; j++)
                    {
                        var quizId = random.Next(1, 101);
                        sequence.Add($"{category.Code}{i + 1}Q{quizId}");
                    }

                    sequence.Add($"{category.Code}{i + 1}");
                }

                _logger.LogInformation("Sequence generation completed successfully.");
                return sequence;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during sequence generation.");
                throw;
            }
        }

        public bool IsValidSequence(List<string> sequence)
        {
            _logger.LogInformation("Starting sequence validation...");
            try
            {
                for (int i = 0; i < sequence.Count - 1; i++)
                {
                    if (sequence[i + 1].StartsWith('Q') && !sequence[i].StartsWith(sequence[i + 1][1].ToString()))
                    {
                        return false;
                    }
                }

                _logger.LogInformation("Sequence validation completed successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during sequence validation.");
                throw;
            }
        }

        public int CalculateScore(List<string> sequence, Dictionary<int, double> quizScores, Dictionary<string, double> courseCompletionRates, double quizPassingThreshold, double coursePassingThreshold, Dictionary<string, int> classWeights)
        {
            _logger.LogInformation("Starting score calculation...");
            int score = 0;

            try
            {
                foreach (var item in sequence)
                {
                    if (item.StartsWith('Q'))
                    {
                        var quizId = int.Parse(item.Substring(2));
                        if (quizScores.TryGetValue(quizId, out var quizScore) && quizScore >= quizPassingThreshold)
                        {
                            score += (int)(quizScore * 100); // Assuming score is out of 100
                        }
                    }
                    else
                    {
                        var category = item[0].ToString();
                        if (courseCompletionRates.TryGetValue(category, out var completionRate) && completionRate >= coursePassingThreshold)
                        {
                            score += classWeights[category];
                        }
                    }
                }

                _logger.LogInformation("Score calculation completed successfully.");
                return score;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during score calculation.");
                throw;
            }
        }

        public string RunAlgorithm()
        {
            _logger.LogInformation("Running genetic algorithm...");
            try
            {
                // Logique de l'algorithme génétique utilisant des données de la base de données
                _logger.LogInformation("Genetic algorithm completed successfully.");
                return "Résultat de l'algorithme génétique";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during genetic algorithm execution.");
                throw;
            }
        }
    }
}
