﻿using DbInterface;
using DbService;
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

        public static List<QualityCertificationReportImg> GetQualityCertificationReportImg(int inComeId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Select<QualityCertificationReportImg>(p => p.incomeId == inComeId);
            }
        }
    }
}
