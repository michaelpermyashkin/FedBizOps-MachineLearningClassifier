using Microsoft.ML;
using System;
using System.IO;

namespace JSON_RFP_Classifier
{
    class RFPClassifierLabel
    {
        private readonly MLContext _mlContext;
        private PredictionEngine<RFPData, RFPPrediction> _predEngine;

        public RFPClassifierLabel()
        {
            _mlContext = new MLContext(seed: 0);
        }

        public void LoadModel(string modelPath)
        {
            ITransformer loadedModel;
            using (var stream = new FileStream(modelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                loadedModel = _mlContext.Model.Load(stream, out var modelInputSchema);
            _predEngine = _mlContext.Model.CreatePredictionEngine<RFPData, RFPPrediction>(loadedModel);
        }

        public string PredictCategory(RFPData transaction)
        {
            var prediction = new RFPPrediction();
            _predEngine.Predict(transaction, ref prediction);
            return prediction?.Office;
        }

        internal IDataView Transform(IDataView testDataView)
        {
            throw new NotImplementedException();
        }
    }
}
