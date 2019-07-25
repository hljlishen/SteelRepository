using DbInterface;
using DbService;
using System.Linq;

namespace Models
{
    public partial class Category : ModelBase
    {
        public static Category GetCategory(string categoryName)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindFirst<Category, string>("categoryName", categoryName);
            }
        }

        public static Category GetCategory(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<Category>(id);
            }
        }

        public static int Insert(Category category)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Insert(category);
            }
        }
    }
}
