using DbInterface;
using DbService;
using System.Collections.Generic;

namespace Models
{
    public partial class RecheckReportImg
    {
        public static RecheckReportImg Insert(int reportId, byte[] imgBytes, IDbInterface dbInterface)
        {
            var img = new RecheckReportImg() { img = imgBytes, reportId = reportId };
            dbInterface.Insert(img, false);
            return img;
        }

        public static List<RecheckReportImg> GetRecheckReportImgs(int reportId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Select<RecheckReportImg>(p => p.reportId == reportId);
            }
        }

        public static int Delect(int recheckImgId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Delete<RecheckReportImg>(recheckImgId);
            }
        }

        public static List<RecheckReportImgView> GetRecheckReportImgViews(int incomeId)
        {
            List<RecheckReportImgView> listDesc = new List<RecheckReportImgView>();
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var ret = helper.SqlQuery<RecheckReportImgView>("select RecheckReportImgView.* from RecheckReportImgView where RecheckReportImgView.incomeId = "+incomeId+" order by RecheckReportImgView.recheckOrderNo desc");
                foreach (var rriv in ret)
                {
                    listDesc.Add(rriv);
                }
                return listDesc;
            }
        }
    }
}
