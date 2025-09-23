using FuzzySharp;
using Microsoft.AspNetCore.Http;
using Microsoft.ML;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OptimumERP.AI.Extensions;
using OptimumERP.AI.Models;
using OptimumERP.AI.Models.PriceQuote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimumERP.AI.Services
{
    public class BestPriceOfferService
    {
        private readonly PredictionEngine<ColumnNameData, ColumnPrediction> _engine;

        public BestPriceOfferService()
        {
            _engine = PredictionEngineFactory.GetPredictionEngine<ColumnNameData, ColumnPrediction>();
        }

        public Result<IEnumerable<ItemPriceQuoteResult>> GetBestPriceOfferService(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return new Result<IEnumerable<ItemPriceQuoteResult>> { Message = "Please upload at least one Excel file." };

            bool areXlsx = files.All(f => Path.GetExtension(f.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase));

            if (!areXlsx)
                return new Result<IEnumerable<ItemPriceQuoteResult>> { Message = "Please upload Excel files." };


            var result = new List<ItemPriceQuoteRaw>();

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

                    var raw = new ItemPriceQuoteRaw();

                    raw.CompanyName = Path.GetFileNameWithoutExtension(file.FileName);

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
                                    raw.Qty = qty;
                                break;
                            case "Price":
                                if (decimal.TryParse(cellValue, out var price))
                                    raw.Price = price;
                                break;
                            case "Discount1":
                                if (decimal.TryParse(cellValue, out var d1))
                                    raw.DiscountRatio = d1;
                                break;
                            case "Discount2":
                                if (decimal.TryParse(cellValue, out var d2))
                                    raw.DiscountRatio2 = d2;
                                break;
                            case "Discount3":
                                if (decimal.TryParse(cellValue, out var d3))
                                    raw.DiscountRatio3 = d3;
                                break;
                        }
                    }

                    result.Add(raw);
                }
            }

            var results = new List<ItemPriceQuoteResult>();
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
                var prices = group.Select(r => new PriceQuoteResult
                {
                    CompanyName = r.CompanyName,
                    DiscountRatio = r.DiscountRatio,
                    DiscountRatio2 = r.DiscountRatio2,
                    DiscountRatio3 = r.DiscountRatio3,
                    Price = r.GetFinalPrice()
                }).ToList();

                // Find lowest price
                var minPrice = prices.Min(p => p.Price);
                foreach (var p in prices)
                {
                    p.IsBest = p.Price == minPrice;
                }

                results.Add(new ItemPriceQuoteResult
                {
                    Item = raw.Item,
                    Prices = prices
                });
            }

            return new Result<IEnumerable<ItemPriceQuoteResult>> { Data = results  , IsSucceed = true } ;

        }

    }
}
