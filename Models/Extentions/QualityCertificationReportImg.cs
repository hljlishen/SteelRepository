using DbInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class QualityCertificationReportImg
    {
        public static QualityCertificationReportImg Insert(int incomeId, byte[] imgBytes, IDbInterface dbInterface)
        {
            QualityCertificationReportImg img = new QualityCertificationReportImg() { incomeId = incomeId, img = imgBytes };
            dbInterface.Insert(img, false);
            return img;
        }
    }
}
