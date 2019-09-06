using DbInterface;
using DbService;
using System.Linq;

namespace Models
{
    public partial class Category
    {
        //public static Category GetCategory(string categoryName)
        //{
        //    using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
        //    {
        //        return helper.FindFirst<Category, string>("categoryName", categoryName);
        //    }
        //}

        //public static Category GetCategory(int id)
        //{
        //    using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
        //    {
        //        return helper.FindId<Category>(id);
        //    }
        //}

        //public static int Insert(Category category)
        //{
        //    using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
        //    {
        //        return helper.Insert(category);
        //    }
        //}

        public static Category Insert(string category)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                var record = helper.FindFirst<Category, string>("categoryName", category);
                if (record != null) return record;
                var ret = new Category() { categoryName = category };
                helper.Insert(ret);
                return ret;
            }
        }
    }
}
