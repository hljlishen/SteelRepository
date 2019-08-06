using DbInterface;
using DbService;

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

        public static Model Insert(string model)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var record = helper.FindFirst<Model, string>("modelName", model);
                if (record != null) return record;
                var ret = new Model() { modelName = model };
                helper.Insert(ret);
                return ret;
            }
        }
    }
}
