using Microsoft.ML.Data;

namespace OptimumERP.AI.Models;

public class ColumnPrediction
{
    [ColumnName("PredictedLabel")]
    public string? PredictedLabel { get; set; }
}