using ClosedXML.Excel;
using SpCollectorsAdminApi;


namespace SpCollectorsAdminApi.Services.Excel
{
    public class ExcelParsel
    {

        public List<CollectorEntry> ParseExcel(Stream stream)
        {
            var workbook = new XLWorkbook(stream);
            var sheet = workbook.Worksheet(1);

            var collectorEntries = new List<CollectorEntry>();
            CollectorEntry? currentCollector = null;

            foreach (var row in sheet.RowsUsed().Where(r => r.RowNumber() >= 18))
            {
                var firstCell = row.Cell(1).GetString().Trim();


                // 📌 Step 1: Identify a collector row (e.g., "ALCANTARA, RAQUEL - (VIRAC25SA100007)")
                if (string.IsNullOrWhiteSpace(firstCell))
                {
                    var maybeName = row.Cell(2).GetString().Trim();
                    if (!string.IsNullOrWhiteSpace(maybeName) && maybeName.Contains("- ("))
                    {
                        var nameParts = maybeName.Split(" - ");
                        var collectorName = nameParts[0].Trim();
                        var collectorCode = nameParts[1].Trim('(', ')');

                        currentCollector = new CollectorEntry
                        {
                            CollectorName = collectorName,
                            CollectorCode = collectorCode
                        };

                        collectorEntries.Add(currentCollector);
                    }

                    continue;
                }

                // 📌 Step 2: Parse plan rows (they start with a row number like "1", "2", etc.)
                if (int.TryParse(firstCell, out _))
                {
                    if (currentCollector == null)
                        continue; // Defensive: skip if no collector defined yet

                    try
                    {
                        var plan = new PlanDetail
                        {
                            Number = row.Cell(1).GetValue<int>(),
                            ContractNo = row.Cell(4).GetString(),
                            Planholder = row.Cell(6).GetString(),
                            Plan = row.Cell(11).GetString(),
                            Description = row.Cell(18).GetString(),
                            EffectiveDate = GetDateTimeValue(row.Cell(14)),
                            DueDate = GetDateTimeValue(row.Cell(15)),
                            QuotaComm = GetDoubleValue(row.Cell(16)),
                            QuotaNComm = GetDoubleValue(row.Cell(19)),
                            CBI = row.Cell(21).GetString(),
                            InstNo = row.Cell(22).GetString(),
                            Aging = row.Cell(23).GetValue<int>(),
                            Balance = GetDoubleValue(row.Cell(25)),
                            Tax = GetDoubleValue(row.Cell(26)),
                            Ins = row.Cell(27).GetString(),
                            ORNo = row.Cell(29).GetString(),
                            ORDate = GetDateTimeValue(row.Cell(31)),
                            CollDue = GetDoubleValue(row.Cell(33)),
                            CollAdvance = GetDoubleValue(row.Cell(35))
                        };

                        currentCollector.Entries.Add(plan);
                    }
                    catch
                    {
                        // Log or handle malformed rows if needed
                        continue;
                    }
                }
            }

            return collectorEntries;
        }

        private double? TryParseDouble(string input)
        {
            if (double.TryParse(input?.Replace(",", "").Trim(), out var val))
                return val;

            return null;
        }

        

        private double? GetDoubleValue(IXLCell cell)
        {
            if (cell.DataType == XLDataType.Number)
            {
                return cell.GetDouble();
            }
            return null;
        }

        // Helper method to get DateTime value from a cell
        private DateTime? GetDateTimeValue(IXLCell cell)
        {
            if (cell.DataType == XLDataType.DateTime)
            {
                return cell.GetDateTime();
            }
            return null;
        }
    }
}





