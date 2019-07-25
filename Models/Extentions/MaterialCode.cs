using DbInterface;
using DbService;
using System.Collections.Generic;

namespace Models
{
    public partial class MaterialCode
    {
        public Name GetName()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<Name>(materialNameId);
            }
        }

        public Model GetModel()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<Model>(materialModelId);
            }
        }

        public static MaterialCode GetMaterialCode(string name, string model)
        {
            var mName = Name.GetName(name);
            var mModel = Model.GetModel(model);

            if (mName == null || mModel == null)    //名称或者规格没有记录，必然没有编号的记录
                return null;
            else   //名称和规格记录都存在
            {
                int nameId = mName.id;
                int modelId = mModel.id;

                List<MaterialCode> codeList;
                using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
                {
                    //查询名称和规格的组合
                    codeList = helper.Select<MaterialCode>(p => p.materialModelId == modelId && p.materialNameId == nameId);
                }

                if (codeList.Count > 0) //组合存在，返回查询结果
                    return codeList[0];
                else   //组合不存在，返回null
                    return null;
            }
        }
    }
}
