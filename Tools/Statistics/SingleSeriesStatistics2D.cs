using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Tools.Statistics
{
    public class SingleSeriesStatistics2D<TObject, TValue> where TObject : class
    {
        public string XAxisName { get; private set; }
        public string YAxisName { get; private set; }

        public Func<IEnumerable<TValue>, dynamic> Calculator;

        public SingleSeriesStatistics2D(string xAxisName, string yAxisName, Func<IEnumerable<TValue>, dynamic> calculator = null)
        {
            XAxisName = xAxisName;
            YAxisName = yAxisName;
            Calculator = calculator ?? (p =>
                 {
                     dynamic sum = default(TValue);
                     foreach (var value in p)
                     {
                         sum += value;
                     }

                     return sum;
                 }); // 默认是所有值相加
        }

        public Dictionary<string, TValue> GetValues(IEnumerable<TObject> objects)
        {
            var seriesValues = GetSingleSeriesData(objects, YAxisName);
            var ret = new Dictionary<string, TValue>();
            foreach (var item in seriesValues)
            {
                ret.Add(item.Key, Calculator.Invoke(item.Value));
            }

            return ret;
        }

        private Dictionary<string, List<TValue>> GetSingleSeriesData(IEnumerable<TObject> objects, string seriesName)
        {
            Type objectType = typeof(TObject);
            if (!TypeContainsProperty(objectType, seriesName))
                throw new Exception($"{objectType.ToString()}类型不包含:\"{seriesName}\"属性");

            Dictionary<string, List<TValue>> ret = new Dictionary<string, List<TValue>>();
            foreach (var item in objects)
            {
                string xAxisValue = objectType.GetProperty(XAxisName).GetValue(item).ToString();

                PropertyInfo propInfo = objectType.GetProperty(seriesName);
                object propertyValue = propInfo.GetValue(item);
                TValue seriesValue;
                if (propertyValue == null)
                    seriesValue = default(TValue);
                else
                {
                    var converter = TypeDescriptor.GetConverter(typeof(TValue));
                    seriesValue = (TValue)converter.ConvertTo(propertyValue, typeof(TValue));
                }

                    if (!ret.ContainsKey(xAxisValue))
                    ret.Add(xAxisValue, new List<TValue>());
                ret[xAxisValue].Add(seriesValue);
            }

            return ret;
        }

        protected bool TypeContainsProperty(Type type, string propertyName)
        {
            PropertyInfo prop = type.GetProperty(propertyName);

            if (prop == null)
                return false;
            return true;
        }
    }
}
