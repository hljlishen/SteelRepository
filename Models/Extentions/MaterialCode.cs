using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using Tools;

namespace Models
{
    public partial class MaterialCode
    {
        private static IDbInterface Dbhelper = new DbHelper(new SteelRepositoryDbEntities());
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

        public static MaterialCode Insert(string code, string name, string model, IDbInterface helper)
        {
            var mCode = GetMaterialCode(code);
            var mName = Name.GetName(name);
            var mModel = Model.GetModel(model);
            var mCodeMatch = GetMaterialCode(name, model);
            if (mName != null && mModel != null && mCodeMatch != null && mCode != null && mCode.id == mCodeMatch.id)   //对应excel第2行,组合正确，不用写入新数据
                return mCode;
            else if (mCode == null)
            {
                if (mCodeMatch != null)     //对应excel第8行
                    throw new Exception("code输入错误");
                else   //对应excel9-12行
                {
                    int nameId = -1;
                    int modelId = -1;
                    //插入没有的数据
                    if (mName == null)
                    {
                        var insertName = new Name() { materialName = name };
                        helper.Insert(insertName);
                        nameId = insertName.id;
                    }
                    else
                    {
                        nameId = mName.id;
                    }
                    if (mModel == null)
                    {
                        var insertModel = new Model() { modelName = model };
                        helper.Insert(insertModel);
                        modelId = insertModel.id;
                    }
                    else
                    {
                        modelId = mModel.id;
                    }
                    //插入MaterialCode
                    var ret = new MaterialCode() { materialModelId = modelId, materialNameId = nameId, code = code };
                    helper.Insert(ret);
                    helper.Commit();
                    return ret;
                }
            }
            else   //对应excel文件3-7行条件
            {
                throw new Exception("输入错误");
            }
        }

        public static MaterialCode GetMaterialCode(string code)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindFirst<MaterialCode, string>("code", code);
            }
        }
        public static List<MaterialCode> GetMaterialCodes(string code)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                if (code == "")
                {
                    return null;
                }
                var a = helper.Select<MaterialCode>(p => p.code == code);
                return a;
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
        public static MaterialCode GetMaterialCode(int codeId)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<MaterialCode>(codeId);
            }
        }

        public static MaterialCode GetMaterialCode(int codeId, IDbInterface helper)
        {
            return helper.FindId<MaterialCode>(codeId);
        }
        public static List<MaterialCode> GetMaterialCodeList()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.SelectAll<MaterialCode>();
            }
        }
        public static List<int> GetMaterialId(string codeinput, string nameinput)
        {
            List<int> codeid = new List<int>();
            foreach (var code in GetMaterialCodes(codeinput))
            {
                foreach (var name in Name.GetNames(nameinput))
                {
                    if (code.materialNameId == name.id)
                    {
                        codeid.Add(code.id);
                    }
                }
            }
            return codeid;
        }
        public static List<InCome> MulSelectCheckInCome(string begin, string end, int MaterCodeid, int manufacturerid)
        {
            ExpressionBuilder<InCome> builder = new ExpressionBuilder<InCome>();
            if (begin != "")
            {
                DateTime begintime = Convert.ToDateTime(begin);
                builder.And(p => p.storageTime >= begintime);
            }
            if (end != "")
            {
                DateTime endtime = Convert.ToDateTime(end);
                builder.And(p => p.storageTime <= endtime);
            }
            if (MaterCodeid != 0)
                builder.And(p => p.codeId == MaterCodeid);
            if (manufacturerid != 0)
                builder.And(p => p.menufactureId == manufacturerid);
            var exp = builder.GetExpression();
            return Dbhelper.Select(exp);
        }
    }
}
