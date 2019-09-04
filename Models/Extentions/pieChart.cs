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
                foreach (var inventoryView in helper.SelectAll<InventoryView>())
                {
                    int isEcistCode = IsExistCode(inventoryView, s);
                    if (isEcistCode == -1)
                    {
                        if (inventoryView.unit == "g")
                            d.Add(inventoryView.amount / 1000);
                        else
                            d.Add(inventoryView.amount);
                        s.Add(inventoryView.code);
                    }
                    else
                    {
                        if (inventoryView.unit == "g")
                            d[isEcistCode] += inventoryView.amount / 1000;
                        else
                            d[isEcistCode] += inventoryView.amount;
                    }
                }
                pieChart.mapName = s;
                pieChart.mapValue = d;
                return pieChart;
            }
        }

        private static int IsExistCode(InventoryView inventoryView, List<string> s)
        {
            for (int i = 0; i < s.Count;i++)
            {
                if (inventoryView.code == s[i])
                    return i;
            }
            return -1;
        }
    }
}
