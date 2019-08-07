using System;
using System.Collections.Generic;

namespace Tools.Statistics
{
    public enum StatisticsType
    {
        Sum,
        Count,
        Average,
        UserDefine
    }
    internal class SingleSeriesStatistics2D<TObject> where TObject : class
    {
        private static Func<IEnumerable<double>, double> SumCalculator = p =>        //计算和值
        {
            double sum = 0;
            foreach (var value in p)
            {
                sum += value;
            }
            return sum;
        }; 
        private static Func<IEnumerable<double>, double> CountCalculator = p =>        //计算个数
        {
            int count = 0;
            foreach (var value in p)
            {
                count++;
            }
            return count;
        };
        private static Func<IEnumerable<double>, double> AverageCalculator = p =>        //计算均值
        {
            double sum = 0;
            int i = 0;
            foreach (var value in p)
            {
                sum += value;
                i++;
            }
            return sum / i;
        };

        private Func<TObject, string> XAxisMapper;
        private Func<TObject, double> YAxisMapper;

        public Func<IEnumerable<double>, double> Calculator;

        public SingleSeriesStatistics2D(Func<TObject, string> xAxisMapper , Func<TObject, double> yAxisMapper, StatisticsType type = StatisticsType.Sum, Func<IEnumerable<double>, double> calculator = null)
        {
            switch (type)
            {
                case StatisticsType.Sum:
                    Calculator = SumCalculator;
                    break;
                case StatisticsType.Count:
                    Calculator = CountCalculator;
                    break;
                case StatisticsType.Average:
                    Calculator = AverageCalculator;
                    break;
                case StatisticsType.UserDefine:
                    if (calculator == null) throw new Exception("传入UserDefine类型时，calculator不能为null");
                    Calculator = calculator;
                    break;
                default:
                    throw new Exception($"错误的StatisticsType类型{type}");
            }

            XAxisMapper = xAxisMapper;
            YAxisMapper = yAxisMapper;
        }

        public Dictionary<string, double> GetValues(IEnumerable<TObject> objects)
        {
            if (objects == null) return new Dictionary<string, double>();

            var seriesValues = GetSingleSeriesData(objects);
            var ret = new Dictionary<string, double>();
            foreach (var item in seriesValues)
            {
                ret.Add(item.Key, Calculator.Invoke(item.Value));
            }

            return ret;
        }

        private Dictionary<string, List<double>> GetSingleSeriesData(IEnumerable<TObject> objects)
        {
            Dictionary<string, List<double>> ret = new Dictionary<string, List<double>>();
            foreach (var item in objects)
            {
                string xAxisValue = XAxisMapper.Invoke(item);
                double seriesValue = YAxisMapper.Invoke(item);

                if (!ret.ContainsKey(xAxisValue))
                    ret.Add(xAxisValue, new List<double>());
                ret[xAxisValue].Add(seriesValue);
            }

            return ret;
        }
    }
}
