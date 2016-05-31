namespace Selly.Website.Models
{
    public class SummaryModel
    {
        public double TotalOrdersValue { get; set; }

        public double TotalIncomeValue { get; set; }

        public int NumberOfOrders { get; set; }

        public int NumberOfClients { get; set; }

        public int NumberOfProducts { get; set; }

        public int NumberOfPayments { get; set; }
    }
}