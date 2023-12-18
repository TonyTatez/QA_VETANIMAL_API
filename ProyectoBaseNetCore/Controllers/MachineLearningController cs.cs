using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using VET_ANIMAL_API.Models;
using VET_ANIMAL_API.Services;

namespace VET_ANIMAL_API.Controllers
{
    [ApiController]
    [Route("api/Prediccion/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MachineLearningController : ControllerBase

    {
        private readonly MachineLearningService _machineLearningService;
        private ITransformer _trainedModel;

        public MachineLearningController(MachineLearningService machineLearningService)
        {
            _machineLearningService = machineLearningService;
        }

        [HttpPost("train")]
        public IActionResult TrainModel([FromBody] List<DogData> trainingData)
        {
            try
            {
                var trainedModel = _machineLearningService.TrainModel(trainingData);
                return Ok("Modelo entrenado con éxito");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al entrenar el modelo: {ex.Message}");
            }
        }
        [HttpPost("predict")]
        public IActionResult MakePrediction([FromBody] DogData inputData)
        {
            try
            {
                // Asegúrate de proporcionar inputData al llamar al método MakePrediction
                var prediction = _machineLearningService.MakePrediction(_trainedModel, inputData);
                return Ok($"Predicción: {prediction}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al realizar la predicción: {ex.Message}");
            }
        }
    }
}