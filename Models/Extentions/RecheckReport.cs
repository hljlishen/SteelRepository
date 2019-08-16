﻿using DbInterface;
using DbService;
using System;
using System.Collections.Generic;

namespace Models
{
    public partial class RecheckReport
    {
        public static RecheckReport Insert(int incomeId, DateTime dateTime, List<byte[]> imgs)
        {
            if (imgs == null)
                throw new Exception("报告图片不能为空，至少上传一张图片");
            var report = new RecheckReport() { incomeId = incomeId, recheckTime = dateTime };
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                helper.Insert(report);
                foreach (var item in imgs)
                {
                    helper.Insert(new RecheckReportImg() { reportId = report.id, img = item });
                }
            }
            return report;
        }

        public static List<InCome> GetExpiredIncomes(int expireDays)
        {
            List<InCome> expireIncome = new List<InCome>();
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                //查找现有库存
                List<Inventory> inventory = helper.Select<Inventory>(p => p.amount > 0);

                //根据库存查找最晚提交的复检报告
                List<RecheckReport> lastRecheckRecports = new List<RecheckReport>();
                foreach (var item in inventory)
                {
                    List<RecheckReport> reports = helper.Select<RecheckReport>(p => p.incomeId == item.incomeId);
                    if (reports.Count > 0) //有复检数据,找到最后提交的报告
                    {
                        reports.Sort(new RecheckReportComparer());
                        lastRecheckRecports.Add(reports[0]);
                    }
                    else  //没有复检数据，要看Income是否过期
                    {
                        var income = helper.FindFirst<InCome, int>("id", item.incomeId);
                        if(((DateTime.Now - income.storageTime).TotalDays > expireDays))
                        {
                            expireIncome.Add(income);
                        }
                    }
                }

                //查找过期的复检报告
                List<RecheckReport> expireReports = new List<RecheckReport>();
                foreach (var item in lastRecheckRecports)
                {
                    if((DateTime.Now - item.recheckTime).TotalDays > expireDays)
                    {
                        expireReports.Add(item);
                    }
                }

                //根据过期复检报告查找入库记录
                foreach (var item in expireReports)
                {
                    var income = helper.FindFirst<InCome, int>("id", item.incomeId);
                    expireIncome.Add(income);
                }

                return expireIncome;
            }
        }

        public static RecheckReport GetRecheckReport(int recheckReportId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<RecheckReport>(recheckReportId);
            }
        }

        public static List<RecheckReport> GetRecheckReports(int inComeId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Select<RecheckReport>(p => p.incomeId == inComeId);
            }
        }
    }

    internal class RecheckReportComparer : IComparer<RecheckReport>
    {
        public int Compare(RecheckReport x, RecheckReport y)
        {
            if (x.recheckTime > y.recheckTime)
                return -1;
            else if (x.recheckTime == y.recheckTime)
                return 0;
            else
                return 1;
        }
    }

}
