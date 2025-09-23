using FuzzySharp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BestPriceOffer.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class PredictController : ControllerBase
    {
        private readonly MLContext _mlContext;
        private readonly PredictionEngine<ColumnNameData, ColumnPrediction> _engine;

        public PredictController()
        {
            _mlContext = new MLContext();

            // Training data (expand with synonyms as needed)
            var trainingData = new List<ColumnNameData>
        {
            // Item
            new ColumnNameData { ColumnText = "Item", Label = "Item" },
            new ColumnNameData { ColumnText = "Product", Label = "Item" },
            new ColumnNameData { ColumnText = "Product Name", Label = "Item" },
            new ColumnNameData { ColumnText = "Article", Label = "Item" },
            new ColumnNameData { ColumnText = "Material", Label = "Item" },
            new ColumnNameData { ColumnText = "Goods", Label = "Item" },
            new ColumnNameData { ColumnText = "Item Code", Label = "Item" },
            // Arabic
            new ColumnNameData { ColumnText = "الصنف", Label = "Item" },
            new ColumnNameData { ColumnText = "المنتج", Label = "Item" },
            new ColumnNameData { ColumnText = "اسم المنتج", Label = "Item" },
            new ColumnNameData { ColumnText = "المادة", Label = "Item" },
            new ColumnNameData { ColumnText = "البضاعة", Label = "Item" },

            // Quantity
            new ColumnNameData { ColumnText = "Quantity", Label = "Quantity" },
            new ColumnNameData { ColumnText = "Qty", Label = "Quantity" },
            new ColumnNameData { ColumnText = "Count", Label = "Quantity" },
            new ColumnNameData { ColumnText = "Number", Label = "Quantity" },
            new ColumnNameData { ColumnText = "Amount", Label = "Quantity" },
            new ColumnNameData { ColumnText = "Units", Label = "Quantity" },
            // Arabic
            new ColumnNameData { ColumnText = "الكمية", Label = "Quantity" },
            new ColumnNameData { ColumnText = "عدد", Label = "Quantity" },
            new ColumnNameData { ColumnText = "العدد", Label = "Quantity" },
            new ColumnNameData { ColumnText = "الكم", Label = "Quantity" },
            new ColumnNameData { ColumnText = "الوحدات", Label = "Quantity" },

            // Price
            new ColumnNameData { ColumnText = "Price", Label = "Price" },
            new ColumnNameData { ColumnText = "Unit Price", Label = "Price" },
            new ColumnNameData { ColumnText = "Cost", Label = "Price" },
            new ColumnNameData { ColumnText = "Unit Cost", Label = "Price" },
            new ColumnNameData { ColumnText = "Selling Price", Label = "Price" },
            new ColumnNameData { ColumnText = "Net Price", Label = "Price" },
            new ColumnNameData { ColumnText = "Base Price", Label = "Price" },
            // Arabic
            new ColumnNameData { ColumnText = "السعر", Label = "Price" },
            new ColumnNameData { ColumnText = "سعر الوحدة", Label = "Price" },
            new ColumnNameData { ColumnText = "التكلفة", Label = "Price" },
            new ColumnNameData { ColumnText = "سعر البيع", Label = "Price" },
            new ColumnNameData { ColumnText = "السعر الصافي", Label = "Price" },

            // Discount1
            new ColumnNameData { ColumnText = "Discount", Label = "Discount1" },
            new ColumnNameData { ColumnText = "Disc1", Label = "Discount1" },
            new ColumnNameData { ColumnText = "Promotion", Label = "Discount1" },
            new ColumnNameData { ColumnText = "Offer", Label = "Discount1" },
            new ColumnNameData { ColumnText = "Rebate", Label = "Discount1" },
            new ColumnNameData { ColumnText = "Markdown", Label = "Discount1" },
            // Arabic
            new ColumnNameData { ColumnText = "الخصم", Label = "Discount1" },
            new ColumnNameData { ColumnText = "خصم1", Label = "Discount1" },
            new ColumnNameData { ColumnText = "عرض", Label = "Discount1" },
            new ColumnNameData { ColumnText = "تخفيض", Label = "Discount1" },

            // Discount2
            new ColumnNameData { ColumnText = "Discount 2", Label = "Discount2" },
            new ColumnNameData { ColumnText = "Disc2", Label = "Discount2" },
            new ColumnNameData { ColumnText = "Extra Discount", Label = "Discount2" },
            new ColumnNameData { ColumnText = "Additional Discount", Label = "Discount2" },
            // Arabic
            new ColumnNameData { ColumnText = "خصم 2", Label = "Discount2" },
            new ColumnNameData { ColumnText = "خصم إضافي", Label = "Discount2" },
            new ColumnNameData { ColumnText = "تخفيض إضافي", Label = "Discount2" },

            // Discount3
            new ColumnNameData { ColumnText = "Discount 3", Label = "Discount3" },
            new ColumnNameData { ColumnText = "Disc3", Label = "Discount3" },
            new ColumnNameData { ColumnText = "Special Discount", Label = "Discount3" },
            new ColumnNameData { ColumnText = "Final Discount", Label = "Discount3" },
            // Arabic
            new ColumnNameData { ColumnText = "خصم 3", Label = "Discount3" },
            new ColumnNameData { ColumnText = "خصم نهائي", Label = "Discount3" },
            new ColumnNameData { ColumnText = "تخفيض خاص", Label = "Discount3" },
        };


            var trainData = _mlContext.Data.LoadFromEnumerable(trainingData);

            // Define pipeline
            var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(ColumnNameData.ColumnText))
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("Label"))
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"))
                .AppendCacheCheckpoint(_mlContext);

            // Train model once in constructor
            var model = pipeline.Fit(trainData);

            _engine = _mlContext.Model.CreatePredictionEngine<ColumnNameData, ColumnPrediction>(model);
        }

        [HttpGet("MapHeaderToLabels")]
        public IActionResult MapHeaderToLabels([FromQuery] string headers)
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
            if (files == null || files.Count == 0)
                return BadRequest("Please upload at least one Excel file.");

            var result = new List<Raw>();

            foreach (var file in files)
            {
                using var stream = file.OpenReadStream();
                IWorkbook workbook = new XSSFWorkbook(stream);
                var sheet = workbook.GetSheetAt(0);

                if (sheet == null || sheet.LastRowNum < 1)
                    continue;

                // Read headers
                var headerRow = sheet.GetRow(0);
                var headerMap = new Dictionary<int, string>();

                for (int i = 0; i < headerRow.LastCellNum; i++)
                {
                    var headerValue = headerRow.GetCell(i)?.ToString()?.Trim();
                    if (!string.IsNullOrEmpty(headerValue))
                    {
                        var prediction = _engine.Predict(new ColumnNameData { ColumnText = headerValue });
                        headerMap[i] = prediction.PredictedLabel ?? "Unknown";
                    }
                }

                // Read data rows
                for (int rowIdx = 1; rowIdx <= sheet.LastRowNum; rowIdx++)
                {
                    var row = sheet.GetRow(rowIdx);
                    if (row == null) continue;

                    var raw = new Raw();

                    foreach (var kvp in headerMap)
                    {
                        var cellValue = row.GetCell(kvp.Key)?.ToString();
                        if (string.IsNullOrEmpty(cellValue)) continue;

                        switch (kvp.Value)
                        {
                            case "Item":
                                raw.Item = cellValue;
                                break;
                            case "Quantity":
                                if (int.TryParse(cellValue, out var qty))
                                    raw.Quantity = qty;
                                break;
                            case "Price":
                                if (decimal.TryParse(cellValue, out var price))
                                    raw.Price = price;
                                break;
                            case "Discount1":
                                if (decimal.TryParse(cellValue, out var d1))
                                    raw.Discount1 = d1;
                                break;
                            case "Discount2":
                                if (decimal.TryParse(cellValue, out var d2))
                                    raw.Discount2 = d2;
                                break;
                            case "Discount3":
                                if (decimal.TryParse(cellValue, out var d3))
                                    raw.Discount3 = d3;
                                break;
                        }
                    }

                    result.Add(raw);
                }
            }





        var results = new List<ItemResult>();
        var processed = new HashSet<string>();

        foreach (var raw in result)
        {
            if (processed.Contains(raw.Item)) continue;

            // Find all items similar to current one (threshold 80%)
            var group = result
                .Where(r => Fuzz.Ratio(r.Item, raw.Item) > 80)
                .ToList();

            // Mark processed
            foreach (var g in group) processed.Add(g.Item);

            // Compute final prices
            var prices = group.Select(r => new PriceResult
            {
                CompanyName = r.CompanyName,
                Discount1 = r.Discount1,
                Discount2 = r.Discount2,
                Discount3 = r.Discount3,
                Price = r.GetFinalPrice()
            }).ToList();

            // Find lowest price
            var minPrice = prices.Min(p => p.Price);
            foreach (var p in prices)
            {
                p.IsBest = p.Price == minPrice;
            }

            results.Add(new ItemResult
            {
                Item = raw.Item,
                Prices = prices
            });
        }

        return Ok(results);

      
        }
    }


#region helper classes
// ML.NET input/output classes
public class ColumnNameData
{
    public string? ColumnText { get; set; }
    public string? Label { get; set; }
}

public class ColumnPrediction
{
    [ColumnName("PredictedLabel")]
    public string? PredictedLabel { get; set; }
    }

public class Raw
{
    public string Item { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount1 { get; set; }
    public decimal Discount2 { get; set; }
    public decimal Discount3 { get; set; }
    public string CompanyName { get; set; }
}
public class PriceResult
{
    public string CompanyName { get; set; }
    public decimal Price { get; set; }
    public decimal Discount1 { get; set; }
    public decimal Discount2 { get; set; }
    public decimal Discount3 { get; set; }
    public bool IsBest { get; set; }
}

public class ItemResult
{
    public string Item { get; set; }
    public List<PriceResult> Prices { get; set; } = new();
}

public static class RawExtensions
{
    public static decimal GetFinalPrice(this Raw raw)
    {
        var totalPrice = raw.Price * raw.Quantity;

        var discValue = (1 - (1 - raw.Discount1) * (1 - raw.Discount2) * (1 - raw.Discount3)) * totalPrice;

        return totalPrice - discValue;
    }
}
#endregion