using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using OptimumERP.AI.Models;
using OptimumERP.AI.Services;


namespace BestPriceOffer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PredictController : ControllerBase
{
    private readonly PredictionEngine<ColumnNameData, ColumnPrediction> _engine;

    public PredictController()
    {
         _engine = PredictionEngineFactory.GetPredictionEngine<ColumnNameData, ColumnPrediction>();
    }

    [HttpGet("GetMatchedData")]
    public IActionResult GetMatchedData(string headers)
    {
        if (string.IsNullOrWhiteSpace(headers))
            return BadRequest("Please provide headers as query string, e.g. ?headers=Product,Qty,Unit Cost");

        var headerList = headers.Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(h => h.Trim())
                                .ToList();

        var result = new Dictionary<string, string>();
        foreach (var header in headerList)
        {
            var prediction = _engine.Predict(new ColumnNameData { ColumnText = header });
            result[header] = prediction.PredictedLabel ?? "Unknown";
        }

        return Ok(result);
    }

    [HttpPost("ExtractData")]
    public IActionResult ExtractData([FromForm] List<IFormFile> files)
    {
        var result = new BestPriceOfferService().GetBestPriceOfferService(files);
        return Ok(result.Data);
    }
}   