using System.Diagnostics.Contracts;
using ClosedXML.Excel;
using NPOI.POIFS.Properties;
using SpCollectorsAdminApi;


namespace SpCollectorsAdminApi.Services.Excel
{
    public class ExcelParsel
    {
        public List<CollectorEntry> ParseExcel(Stream stream, string filename)
        {
            var workbook = new XLWorkbook(stream);
            var sheet = workbook.Worksheet(1);
            PlanDetail? lastPlan = null;
            var collectorEntries = new List<CollectorEntry>();
            CollectorEntry? currentCollector = null;

            foreach (var row in sheet.RowsUsed().Where(r => r.RowNumber() >= 18))
            {
                var cell1 = row.Cell(1).GetString().Trim();
                var cell2 = row.Cell(2).GetString().Trim();

                if (row.Cell(4).GetString().Trim().Equals("Accounts", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                if (lastPlan != null &&
                     string.IsNullOrWhiteSpace(row.Cell(1).GetString()) &&
                     string.IsNullOrWhiteSpace(row.Cell(2).GetString()) &&
                     !string.IsNullOrWhiteSpace(row.Cell(43).GetString()))
                {
                    var additionalPayment = new PaymentDetail
                    {
                        ORNo = row.Cell(43).GetString(),
                        ORDate = GetDateTimeValue(row.Cell(45)),
                        CollDue = GetDoubleValue(row.Cell(49)),
                        CollAdvance = GetDoubleValue(row.Cell(53))
                    };

                    lastPlan.Payments.Add(additionalPayment);
                    continue;
                }


             
                if (string.IsNullOrWhiteSpace(cell1))
                {
                    var maybeName = cell2;
                    if (!string.IsNullOrWhiteSpace(maybeName) && maybeName.Contains("- ("))
                    {
                        var nameParts = maybeName.Split(" - ");
                        var collectorName = nameParts[0].Trim();
                        var collectorCode = nameParts[1].Trim('(', ')');

                        currentCollector = new CollectorEntry
                        {
                            CollectorName = collectorName,
                            CollectorCode = collectorCode,
                            Period = ExcelUploadService.ExtractPeriodFromFilename(filename)
                        };

                        collectorEntries.Add(currentCollector);
                    }

                    continue;
                }

                

                // 📌 Step 3: New PlanDetail Entry
                if (int.TryParse(cell1, out _))
                {
                    if (currentCollector == null) continue;

                    try
                    {
                        var plan = new PlanDetail
                        {
                            Number = row.Cell(1).GetValue<int>(),
                            ContractNo = row.Cell(4).GetString(),
                            Planholder = row.Cell(9).GetString(),
                            Plan = row.Cell(15).GetString(),
                            Description = row.Cell(18).GetString(),
                            EffectiveDate = GetDateTimeValue(row.Cell(23)),
                            DueDate = GetDateTimeValue(row.Cell(24)),
                            QuotaComm = GetDoubleValue(row.Cell(27)),
                            QuotaNComm = GetDoubleValue(row.Cell(30)),
                            CBI = GetDoubleValue(row.Cell(34)),
                            InstNo = row.Cell(35).GetString(),
                            Aging = row.Cell(36).GetValue<int?>(),
                            Balance = GetDoubleValue(row.Cell(38)),
                            Tax = row.Cell(41).GetString(),
                            Ins = row.Cell(42).GetString(),
                            CollectorName = currentCollector.CollectorName,
                            CollectorCode = currentCollector.CollectorCode,
                            Period = currentCollector.Period
                        };

                        plan.Payments.Add(new PaymentDetail
                        {
                            ORNo = row.Cell(43).GetString(),
                            ORDate = GetDateTimeValue(row.Cell(45)),
                            CollDue = GetDoubleValue(row.Cell(49)),
                            CollAdvance = GetDoubleValue(row.Cell(53))
                        });

                        currentCollector.Entries.Add(plan);
                        lastPlan = plan;
                    }
                    catch
                    {
                        // Log or skip malformed rows
                        continue;
                    }
                }
            }

            return collectorEntries;
        }

        private double? GetDoubleValue(IXLCell cell)
        {
            // Try reading numeric value directly if it's a number
            if (cell.DataType == XLDataType.Number)
                return cell.GetDouble();

            // Try parsing formatted string fallback
            var raw = cell.GetFormattedString().Trim();

            if (string.IsNullOrWhiteSpace(raw) || raw == "-")
                return null;

            // Handle formatted negative values like (1,234.00)
            raw = raw.Replace(",", "").Replace("(", "-").Replace(")", "");

            if (double.TryParse(raw, out var result))
                return result;

            Console.WriteLine($"⚠️ Failed to parse double from: '{raw}'");
            return null;
        }

        private DateTime? GetDateTimeValue(IXLCell cell)
        {
            // Try direct cast if the cell is a DateTime
            if (cell.DataType == XLDataType.DateTime)
                return cell.GetDateTime();

            // Fallback: try parsing as string
            var raw = cell.GetFormattedString().Trim();

            if (string.IsNullOrWhiteSpace(raw) || raw == "-")
                return null;

            if (DateTime.TryParse(raw, out var parsed))
                return parsed;

            Console.WriteLine($"⚠️ Failed to parse date from: '{raw}'");
            return null;
        }

    }
}





