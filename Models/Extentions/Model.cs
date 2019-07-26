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

        public static void Insert(string model, IDbInterface helper)
        {
            var record = helper.FindFirst<Model, string>("modelName", model);
            if (record != null) return;
            helper.Insert(new Model() { modelName = model }, false);
        }
    }
}
