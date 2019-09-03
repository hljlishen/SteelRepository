using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class pieChart
    {
        public static pieChart GetPieCharts()
        {
            pieChart pieChart = new pieChart();
            List<double> d = new List<double>();
            List<string> s = new List<string>();
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                foreach (var inComeView in helper.SelectAll<InComeView>())
                {
                    int isEcistCode = IsExistCode(inComeView, s);
                    if (isEcistCode == -1)
                    {
                        if (inComeView.unit == "g")
                            d.Add(inComeView.amount / 1000);
                        else
                            d.Add(inComeView.amount);
                        s.Add(inComeView.code);
                    }
                    else
                    {
                        if (inComeView.unit == "g")
                            d[isEcistCode] += inComeView.amount / 1000;
                        else
                            d[isEcistCode] += inComeView.amount;
                    }
                }
                pieChart.mapName = s;
                pieChart.mapValue = d;
                return pieChart;
            }
        }

        private static int IsExistCode(InComeView inComeView,List<string> s)
        {
            for (int i = 0; i < s.Count;i++)
            {
                if (inComeView.code == s[i])
                    return i;
            }
            return -1;
        }
    }
}
