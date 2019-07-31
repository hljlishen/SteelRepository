using System;
using System.Collections.Generic;

namespace Tools.Statistics
{
    public class MultipleSeriesStatistics2D<TObject> where TObject : class
    {
        private Dictionary<string, SingleSeriesStatistics2D<TObject>> statistics = new Dictionary<string, SingleSeriesStatistics2D<TObject>>();
        private Func<TObject, string> XAxisMapper;

        public MultipleSeriesStatistics2D(Func<TObject, string> xAxisMapper)
        {
            XAxisMapper = xAxisMapper;
        }

        public int SeriesCount { get => statistics.Count; }

        public void AddSeries(string seriesName, Func<TObject, double> yAxisMapper, StatisticsType type = StatisticsType.Sum, Func<IEnumerable<double>, double> calculator = null)
        {
            SingleSeriesStatistics2D<TObject> sta = new SingleSeriesStatistics2D<TObject>(XAxisMapper, yAxisMapper, type, calculator);
            statistics.Add(seriesName, sta);
        }

        public Dictionary<string, Dictionary<string, double>> GetValues(IEnumerable<TObject> objects)
        {
            var ret = new Dictionary<string, Dictionary<string, double>>();

            foreach (var pair in statistics)
            {
                ret.Add(pair.Key, pair.Value.GetValues(objects));
            }

            return ret;
        }
    }
}
