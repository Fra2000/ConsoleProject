using System.Globalization;
using CsvHelper;

namespace ConsoleProject
{
    internal class CsvProcessor
    {
        public List<Order> LoadOrdersFromCsv(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var orders = csv.GetRecords<Order>().ToList();
                return orders;
            }
        }
    }
}
