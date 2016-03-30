using System;

namespace Shared.Html.Charts
{
    public class ChartDataSeries
    {
        public ChartDataSeries(string caption, string[] seriesData)
        {
            object[] tmp = new string[seriesData.Length + 1];
            tmp[0] = caption;
            Array.Copy(seriesData, 0, tmp, 1, seriesData.Length);
            column = tmp;
        }
        public ChartDataSeries(string caption, int?[] seriesData)
        {
            object[] tmp = new object[seriesData.Length + 1];
            tmp[0] = caption;
            Array.Copy(seriesData, 0, tmp, 1, seriesData.Length);
            column = tmp;
        }
        public ChartDataSeries(string caption, int[] seriesData)
        {
            object[] tmp = new object[seriesData.Length + 1];
            tmp[0] = caption;
            Array.Copy(seriesData, 0, tmp, 1, seriesData.Length);
            column = tmp;
        }
        public ChartDataSeries(string caption, decimal?[] seriesData)
        {
            object[] tmp = new object[seriesData.Length + 1];
            tmp[0] = caption;
            Array.Copy(seriesData, 0, tmp, 1, seriesData.Length);
            column = tmp;
        }
        public ChartDataSeries(string caption, decimal[] seriesData)
        {
            object[] tmp = new object[seriesData.Length + 1];
            tmp[0] = caption;
            Array.Copy(seriesData, 0, tmp, 1, seriesData.Length);
            column = tmp;
        }
        public ChartDataSeries(string caption, string axisid, string[] seriesData)
        {
            object[] tmp = new string[seriesData.Length + 2];
            tmp[1] = caption;
            tmp[0] = axisid;
            Array.Copy(seriesData, 0, tmp, 2, seriesData.Length);
            column = tmp;
        }
        public ChartDataSeries(string caption, string axisid, int[] seriesData)
        {
            object[] tmp = new object[seriesData.Length + 2];
            tmp[1] = caption;
            tmp[0] = axisid;
            Array.Copy(seriesData, 0, tmp, 2, seriesData.Length);
            column = tmp;
        }

        public ChartDataSeries(string caption, object[] seriesData)
        {
            object[] tmp = new object[seriesData.Length + 1];
            tmp[0] = caption;
            Array.Copy(seriesData, 0, tmp, 1, seriesData.Length);
            column = tmp;
        }
        public object[] column { get; set; }
    }
}