using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ModelBase : IDisposable
    {
        protected IDbInterface helper;

        protected ModelBase()
        {
            helper = new DbHelper(new SteelRepositoryDbEntities());
        }

        public void Dispose()
        {
            helper.Dispose();
        }
    }
}
