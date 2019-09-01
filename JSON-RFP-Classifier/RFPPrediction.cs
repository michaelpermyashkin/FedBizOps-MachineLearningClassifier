using Microsoft.ML.Data;

namespace JSON_RFP_Classifier
{
    class RFPPrediction
    {
        [ColumnName("PredictedLabel")]
        public string Office;
    }
}
