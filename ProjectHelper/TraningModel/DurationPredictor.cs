using Microsoft.ML;

namespace TraningModel
{
    public class DurationPredictor
    {
        private readonly string _modelPath = "MLModels/task_duration_model.zip";
        private readonly MLContext _mlContext;
        private readonly PredictionEngine<TaskTrainingData, TaskPrediction> _predictionEngine;

        public DurationPredictor()
        {
            _mlContext = new MLContext();
            DataViewSchema modelSchema;
            var model = _mlContext.Model.Load(_modelPath, out modelSchema);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<TaskTrainingData, TaskPrediction>(model);
        }

        public float PredictDuration(TaskTrainingData input)
        {
            return _predictionEngine.Predict(input).PredictedDuration;
        }
    }
}
