namespace GreenFever.Invoices.Client.Dto
{
    public class ProcessingPercentageInfo
    {
        public double Percent { get; set; }

        public int ProcessedItemsCount { get; set; }

        public int TotalItemsCount { get; set; }
    }
}
