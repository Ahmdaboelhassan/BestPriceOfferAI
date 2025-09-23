using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;
using OptimumERP.AI.Models;
using OptimumERP.AI.TrainingData;

public static class PredictionEngineFactory
{
    // Build dynamic path to AIModels/ClassificationModel/classificationModel.zip
    private static readonly string ModelPath = Path.GetFullPath(
          Path.Combine("..", "OptimumERP.AI","AIModels", "classificationModel.zip")
      );
    static string ss = Directory.GetCurrentDirectory();
    public static PredictionEngine<T, R> GetPredictionEngine<T, R>()
        where T : class
        where R : class, new()
    {
        var mlContext = new MLContext();

        ITransformer model;

        if (File.Exists(ModelPath))
        {
            // Load existing model
            using var stream = new FileStream(ModelPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            model = mlContext.Model.Load(stream, out _);
        }
        else
        {
            // Train new model
            var trainData = mlContext.Data.LoadFromEnumerable(ClassificationTrainingData.GetData);

            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(ColumnNameData.ColumnText))
                .Append(mlContext.Transforms.Conversion.MapValueToKey("Label"))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"))
                .AppendCacheCheckpoint(mlContext);

            model = pipeline.Fit(trainData);

            // Save the model
            using var stream = new FileStream(ModelPath, FileMode.Create, FileAccess.Write, FileShare.Write);
            mlContext.Model.Save(model, trainData.Schema, stream);
        }

        return mlContext.Model.CreatePredictionEngine<T, R>(model);
    }
}
