using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VET_ANIMAL_API.Models;
using ProyectoBaseNetCore;

namespace VET_ANIMAL_API.Services
{
    public class MachineLearningService
    {
        private readonly MLContext _mlContext;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public MachineLearningService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _mlContext = new MLContext();
        }

        public ITransformer TrainModel(List<DogData> trainingData)
        {
            // Convertir la lista de DogData a un IDataView
            var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

            // Configura el pipeline y entrena el modelo
            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("Resultado")
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("Resultado"))
                .Append(_mlContext.Transforms.Concatenate("Features", "Resultado"));

            var trainedModel = pipeline.Fit(dataView);

            // Guardar el modelo entrenado para usarlo en predicciones posteriores
            var modelPath = GetModelPath();
            _mlContext.Model.Save(trainedModel, dataView.Schema, modelPath);

            return trainedModel;
        }

        public string MakePrediction(ITransformer model, DogData inputData)
        {
            // Haz una predicción con el modelo entrenado
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<DogData, DogPrediction>(model);
            var prediction = predictionEngine.Predict(inputData);

            return prediction.Resultado;
        }

        private string GetModelPath()
        {
            // Lógica para obtener la ruta del modelo entrenado
            // Puedes basarte en la configuración, en un almacenamiento persistente, etc.
            // En este ejemplo, se guarda en la carpeta de la aplicación
            var modelFileName = "trained_model.zip";
            var modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, modelFileName);

            return modelPath;
        }
    }

    public class DogPrediction
    {
        [ColumnName("PredictedLabel")]
        public string Resultado;
    }
}