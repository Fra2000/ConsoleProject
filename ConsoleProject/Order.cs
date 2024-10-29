namespace ConsoleProject
{
    public class Order
    {
        public int Id { get; set; }
        public string ArticleName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal PercentageDiscount { get; set; }
        public string Buyer { get; set; }
    }

}
