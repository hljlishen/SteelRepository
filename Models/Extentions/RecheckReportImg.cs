using DbInterface;

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
    }
}
