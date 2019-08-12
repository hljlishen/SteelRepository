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
    }
}
