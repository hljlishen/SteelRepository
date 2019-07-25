﻿using DbInterface;
using DbService;
using System;
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

        public static void Insert(string code, string name, string model, IDbInterface helper)
        {
            var mCode = GetMaterialCode(code);
            var mName = Name.GetName(name);
            var mModel = Model.GetModel(model);
            var mCodeMatch = GetMaterialCode(name, model);
            if (mName != null && mModel != null && mCode.id == mCodeMatch.id)   //组合正确，不用写入新数据，对应excel第2行
                return;
            else if(mCode == null)
            {
                if (mCodeMatch != null)     //对应excel第8行
                    throw new Exception("输入错误");
                else   //对应excel9-12行
                {
                    //插入没有的数据
                    Name.Insert(name, helper);
                    Model.Insert(model, helper);
                    Insert(code, helper);
                    helper.Commit();
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

        public static void Insert(string code, IDbInterface helper)
        {
            var record = helper.FindFirst<MaterialCode, string>("code", code);
            if (record != null) return;
            helper.Insert(new MaterialCode() { code = code }, false);
        }
    }
}
