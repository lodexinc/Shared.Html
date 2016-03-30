namespace Shared.Html.Charts
{
    public class ChartDataSeriesCollection
    {
        public ChartDataSeriesCollection(string seriesCollectionName, ChartDataSeries[] series)
        {
            x = "x";
            this.SeriesCollectionName = seriesCollectionName;
            this.DataSeries = series;
        }
        public string x { get; set; }
        public string SeriesCollectionName { get; set; }
        public ChartDataSeries[] DataSeries { get; set; }
    }
}