using BestPriceOffer.Helpers;
using FuzzySharp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OptimumERP.AI.Models;
using OptimumERP.AI.Models.PriceQuote;
using OptimumERP.AI.TrainingData;
using OptimumERP.AI.Extensions;
using OptimumERP.AI.Services;


namespace BestPriceOffer.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class PredictController : ControllerBase
    {
 
    [HttpPost("ExtractData")]
    public IActionResult ExtractData([FromForm] List<IFormFile> files)
    {
            var result = new BestPriceOfferService().GetBestPriceOfferService(files);
            return Ok(result.Data);
        }
    }   