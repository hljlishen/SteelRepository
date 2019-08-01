using DbInterface;
using DbService;

namespace Models
{
    public partial class Position
    {
        public static Position GetPosition(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<Position>(id);
            }
        }
        public static int Insert(Position position)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Insert<Position>(position);
            }
        }
        public static int Update(Position position)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.Update(position);
            }
        }
    }
}
