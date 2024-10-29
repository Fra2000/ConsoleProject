namespace ConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var csvProcessor = new CsvProcessor();
            var filePath = "percorso\\file\\csv";

            List<Order> orders = csvProcessor.LoadOrdersFromCsv(filePath);

            // Trova tutti i record con l'importo totale più alto
            var maxTotal = orders.Max(order => CalculateTotalWithDiscount(order));
            var highestTotalOrders = orders
                .Where(order => CalculateTotalWithDiscount(order) == maxTotal)
                .OrderBy(order => order.Id)
                .ToList();


            // Trova tutti i record con la quantità più alta
            var maxQuantity = orders.Max(order => order.Quantity);
            var highestQuantityOrders = orders
                .Where(order => order.Quantity == maxQuantity)
                .OrderBy(order => order.Id)
                .ToList();

            // Trova tutti i record con la maggiore differenza tra totale senza sconto e con sconto
            var maxDifference = orders.Max(order => CalculateDiscountDifference(order));
            var highestDifferenceOrders = orders
                .Where(order => CalculateDiscountDifference(order) == maxDifference)
                .OrderBy(order => order.Id)
                .ToList();

            // Stampa i risultati
            Console.WriteLine("Record con importo totale più alto:");
            Console.WriteLine(new string('-', 20));
            PrintOrders(highestTotalOrders);

            Console.WriteLine("\nRecord con quantità più alta:");
            Console.WriteLine(new string('-', 20));
            PrintOrders(highestQuantityOrders, showTotals: false);

            Console.WriteLine("\nRecord con maggior differenza tra totale senza sconto e totale con sconto:");
            Console.WriteLine(new string('-', 20));
            PrintOrders(highestDifferenceOrders, difference: true);
        }

        // Funzione per calcolare il totale con lo sconto applicato
        private static decimal CalculateTotalWithDiscount(Order order)
        {
            var discountMultiplier = 1 - (order.PercentageDiscount / 100);
            return order.Quantity * order.UnitPrice * discountMultiplier;
        }

        // Funzione per calcolare la differenza tra il totale senza sconto e con sconto
        private static decimal CalculateDiscountDifference(Order order)
        {
            var totalWithoutDiscount = order.Quantity * order.UnitPrice;
            var totalWithDiscount = CalculateTotalWithDiscount(order);
            return totalWithoutDiscount - totalWithDiscount;
        }

        // Funzione per stampare i dettagli di più ordini
        private static void PrintOrders(List<Order> orders, bool showTotals = true, bool difference = false)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                var order = orders[i];
                Console.WriteLine($"Id: {order.Id}");
                Console.WriteLine($"Article Name: {order.ArticleName}");
                Console.WriteLine($"Quantity: {order.Quantity}");
                Console.WriteLine($"Unit Price: {order.UnitPrice}");
                Console.WriteLine($"Percentage Discount: {order.PercentageDiscount}");
                Console.WriteLine($"Buyer: {order.Buyer}");

                if (showTotals)
                {
                    Console.WriteLine($"Total without Discount: {(order.Quantity * order.UnitPrice):F2}");
                    Console.WriteLine($"Total with Discount: {CalculateTotalWithDiscount(order):F2}");
                }

                if (difference)
                {
                    Console.WriteLine($"Difference: {((order.Quantity * order.UnitPrice) - CalculateTotalWithDiscount(order)):F2}");
                }


                // Aggiunge una linea di separazione solo se ci sono più record e non siamo sull'ultimo
                if (orders.Count > 1 && i < orders.Count - 1)
                {
                    Console.WriteLine(new string('-', 10));
                }
            }
        }
    }
}
