namespace GreenFever.Invoice.Dto.Objects
{
    public class ProcessingPercentageInfo
    {
        public double Percent { get; set; }

        public int ProcessedItemsCount { get; set; }

        public int TotalItemsCount { get; set; }
    }
}
