using DbInterface;
using DbService;
using System;
using System.Collections.Generic;

namespace Models
{
    public partial class RecheckReport
    {
        public static IEnumerable<RecheckReport> GetExpiredRports(int? expireDays)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Select<RecheckReport>(p => System.Data.Entity.DbFunctions.DiffDays(DateTime.Now ,p.recheckTime) < -expireDays);
            }
        }
    }
}
