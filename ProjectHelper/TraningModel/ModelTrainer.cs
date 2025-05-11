using Microsoft.ML;

namespace TraningModel
{
    public class ModelTrainer
    {
        private readonly string _modelPath = "MLModels/task_duration_model.zip";

        public void TrainModel(IEnumerable<TaskTrainingData> trainingData)
        {
            var mlContext = new MLContext();
            var dataView = mlContext.Data.LoadFromEnumerable(trainingData);

            var pipeline = mlContext.Transforms.Concatenate("Features",
                    nameof(TaskTrainingData.Complexity),
                    nameof(TaskTrainingData.SkillMatch),
                    nameof(TaskTrainingData.EmployeeExperience),
                    nameof(TaskTrainingData.CurrentLoad))
                .Append(mlContext.Regression.Trainers.FastTree());

            var model = pipeline.Fit(dataView);

            mlContext.Model.Save(model, dataView.Schema, _modelPath);
        }
    }
}
