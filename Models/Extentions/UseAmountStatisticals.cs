using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Statistics;

namespace Models
{
    public partial class UseAmountStatisticals
    {
        public static int[] GetWebClickNumber(DateTime dateTime)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                int January = 0, February = 0, March = 0, April = 0, May = 0, June = 0, July = 0, August = 0, September = 0, October = 0, November = 0, December = 0;
                foreach (var item in helper.SelectAll<UseAmountStatisticals>())
                {
                    if (item.useTime.Year != dateTime.Year) continue;
                    if (item.useTime.Month == 1) January += item.userUse;
                    if (item.useTime.Month == 2) February += item.userUse;
                    if (item.useTime.Month == 3) March += item.userUse;
                    if (item.useTime.Month == 4) April += item.userUse;
                    if (item.useTime.Month == 5) May += item.userUse;
                    if (item.useTime.Month == 6) June += item.userUse;
                    if (item.useTime.Month == 7) July += item.userUse;
                    if (item.useTime.Month == 8) August += item.userUse;
                    if (item.useTime.Month == 9) September += item.userUse;
                    if (item.useTime.Month == 10) October += item.userUse;
                    if (item.useTime.Month == 11) November += item.userUse;
                    if (item.useTime.Month == 12) December += item.userUse;
                }
                int[] myArray = new int[12] { January, February, March, April, May, June, July, August, September, October, November, December };

                return myArray;
            }
        }

        public static Dictionary<string, int> GetNowWebClickNumber()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
                int totalVisits = 0;
                int administratorVisits = 0;
                foreach (var item in helper.SelectAll<UseAmountStatisticals>())
                {
                    totalVisits += item.userUse;
                    administratorVisits += item.administratorLogin;
                }
                keyValuePairs.Add("totalVisits", totalVisits);
                keyValuePairs.Add("administratorVisits", administratorVisits);
                return keyValuePairs;
            }
        }

        public static void AddTraffic(DateTime dateTime)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                //判断时间是否存在
                //不存在新加，存在修改
                UseAmountStatisticals useAmountStatisticals = helper.FindFirst<UseAmountStatisticals, DateTime>("useTime", dateTime.Date);
                UseAmountStatisticals useAmountStatisticals1 = new UseAmountStatisticals();
                if (useAmountStatisticals == null)
                {
                    useAmountStatisticals1.userUse = 1;
                    useAmountStatisticals1.administratorLogin = 0;
                    useAmountStatisticals1.useTime = dateTime.Date;
                    helper.Insert(useAmountStatisticals1);
                }
                else
                {
                    useAmountStatisticals.userUse++;
                    helper.Update(useAmountStatisticals);
                }
            }
        }

        public static void AddAdministratorTraffic(DateTime dateTime)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                //判断时间是否存在
                //不存在新加，存在修改
                UseAmountStatisticals useAmountStatisticals = helper.FindFirst<UseAmountStatisticals, DateTime>("useTime", dateTime.Date);
                UseAmountStatisticals useAmountStatisticals1 = new UseAmountStatisticals();
                if (useAmountStatisticals == null)
                {
                    useAmountStatisticals1.userUse = 0;
                    useAmountStatisticals1.administratorLogin = 1;
                    useAmountStatisticals1.useTime = dateTime.Date;
                    helper.Insert(useAmountStatisticals1);
                }
                else
                {
                    useAmountStatisticals.administratorLogin++;
                    helper.Update(useAmountStatisticals);
                }
            }
        }
    }
}
