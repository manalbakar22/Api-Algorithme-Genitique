using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using ApiGenitique.Service;

namespace ApiGenitique.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneticAlgorithmController : ControllerBase
    {
        private readonly GeneticAlgorithmService _geneticAlgorithmService;
        private readonly ILogger<GeneticAlgorithmController> _logger;

        public GeneticAlgorithmController(GeneticAlgorithmService geneticAlgorithmService, ILogger<GeneticAlgorithmController> logger)
        {
            _geneticAlgorithmService = geneticAlgorithmService;
            _logger = logger;
        }

        [HttpGet("run")]
        public IActionResult RunAlgorithm()
        {
            try
            {
                _logger.LogInformation("Running genetic algorithm...");
                var result = _geneticAlgorithmService.RunAlgorithm();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while running genetic algorithm.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("generate-sequences")]
        public IActionResult GenerateSequences(int length = 10, int maxQuizPerCategory = 5)
        {
            if (length <= 0 || maxQuizPerCategory < 0)
            {
                return BadRequest("Invalid input parameters.");
            }

            try
            {
                _logger.LogInformation("Generating sequences...");
                var sequences = _geneticAlgorithmService.GenerateSequence(length, maxQuizPerCategory);
                return Ok(sequences);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating sequences.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("validate-sequence")]
        public IActionResult ValidateSequence([FromBody] List<string> sequence)
        {
            if (sequence == null || sequence.Count == 0)
            {
                return BadRequest("Sequence cannot be null or empty.");
            }

            try
            {
                _logger.LogInformation("Validating sequence...");
                var isValid = _geneticAlgorithmService.IsValidSequence(sequence);
                return Ok(isValid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while validating sequence.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("calculate-score")]
        public IActionResult CalculateScore([FromBody] CalculateScoreRequest request)
        {
            if (request == null || request.Sequence == null || request.QuizScores == null || request.CourseCompletionRates == null || request.ClassWeights == null)
            {
                return BadRequest("Request data is invalid.");
            }

            try
            {
                _logger.LogInformation("Calculating score...");
                var score = _geneticAlgorithmService.CalculateScore(request.Sequence, request.QuizScores, request.CourseCompletionRates, request.QuizPassingThreshold, request.CoursePassingThreshold, request.ClassWeights);
                return Ok(score);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while calculating score.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class CalculateScoreRequest
    {
        public List<string> Sequence { get; set; }
        public Dictionary<int, double> QuizScores { get; set; }
        public Dictionary<string, double> CourseCompletionRates { get; set; }
        public double QuizPassingThreshold { get; set; }
        public double CoursePassingThreshold { get; set; }
        public Dictionary<string, int> ClassWeights { get; set; }
    }
}
