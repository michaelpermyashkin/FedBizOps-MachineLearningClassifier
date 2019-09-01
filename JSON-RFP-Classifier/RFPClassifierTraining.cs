using Microsoft.ML;
using System.Collections.Generic;
using System.Linq;

namespace JSON_RFP_Classifier
{
    class RFPClassifierTraining
    {
        public void Train(IEnumerable<RFPData> trainingData, string modelSavePath)
        {
            var mlContext = new MLContext(seed: 0);

            // Configure ML pipeline
            var pipeline = LoadDataProcessPipeline(mlContext);
            var trainingPipeline = GetTrainingPipeline(mlContext, pipeline);
            var trainingDataView = mlContext.Data.LoadFromEnumerable(trainingData);

            // Generate training model.
            var trainingModel = trainingPipeline.Fit(trainingDataView);

            // Save training model to disk.
            mlContext.Model.Save(trainingModel, trainingDataView.Schema, modelSavePath);
        }

        private IEstimator<ITransformer> LoadDataProcessPipeline(MLContext mlContext)
        {
            // Configure data pipeline based on the features in TransactionData.
            // Description and TransactionType are the inputs and Category is the expected result.
            var dataProcessPipeline = mlContext
                .Transforms.Conversion.MapValueToKey(inputColumnName: nameof(RFPData.Office), outputColumnName: "Label")
                .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: nameof(RFPData.Agency), outputColumnName: "AgencyFeaturized"))
                .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: nameof(RFPData.ClassCode), outputColumnName: "ClassCodeFeaturized"))
                .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: nameof(RFPData.Naics), outputColumnName: "NaicsFeaturized"))
                .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: nameof(RFPData.Subject), outputColumnName: "SubjectFeaturized"))
                .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: nameof(RFPData.SolNbr), outputColumnName: "SolNbrFeaturized"))
                .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: nameof(RFPData.SetAside), outputColumnName: "SetAsideFeaturized"))
                // Merge features into a single feature.
                .Append(mlContext.Transforms.Concatenate("Features", "AgencyFeaturized", "ClassCodeFeaturized", "NaicsFeaturized", "SubjectFeaturized", "SolNbrFeaturized", "SetAsideFeaturized"))
                .AppendCacheCheckpoint(mlContext);

            return dataProcessPipeline;
        }

        private IEstimator<ITransformer> GetTrainingPipeline(MLContext mlContext, IEstimator<ITransformer> pipeline)
        {
            // Use the multi-class SDCA algorithm to predict the label using features.
            // For StochasticDualCoordinateAscent the KeyToValue needs to be PredictedLabel.
            return pipeline
                .Append(GetScadaTrainer(mlContext))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
        }

        private IEstimator<ITransformer> GetScadaTrainer(MLContext mlContext)
        {
            return mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features");
        }
    }
}
