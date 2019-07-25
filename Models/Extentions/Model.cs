using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class Model
    {
        public static Model GetModel(string model)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindFirst<Model, string>("modelName", model);
            }
        }

        public static void Insert(string model, IDbInterface helper)
        {
            var record = helper.FindFirst<Model, string>("modelName", model);
            if (record != null) return;
            helper.Insert(new Model() { modelName = model }, false);
        }
    }
}
