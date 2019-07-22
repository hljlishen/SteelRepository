using System;
using System.Collections.Generic;

namespace Tools.Statistics
{
    public class MultipleSeriesStatistics2D<TObject> where TObject : class
    {
        private Dictionary<string, SingleSeriesStatistics2D<TObject, double>> statistics = new Dictionary<string, SingleSeriesStatistics2D<TObject, double>>();

        public MultipleSeriesStatistics2D(string xAxisName)
        {
            XAxisName = xAxisName;
        }

        public int SeriesCount { get => statistics.Count; }

        public string XAxisName { get; private set; }
        public void AddSeries(string yAxisName, Func<IEnumerable<double>, dynamic> calculator = null)
        {
            if (statistics.ContainsKey(yAxisName)) throw new Exception($"已经包含了{yAxisName}属性的统计，不能重复添加");

            SingleSeriesStatistics2D<TObject, double> sta = new SingleSeriesStatistics2D<TObject, double>(XAxisName, yAxisName, calculator);
            statistics.Add(yAxisName, sta);
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
