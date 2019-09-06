using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class Tree
    {
        public static List<Tree> GetCategoryName ()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                List<Tree> listTree = new List<Tree>();
                foreach (var category in helper.SelectAll<Category>())
                {
                    if (category != null)
                    {
                        Tree tree = new Tree();
                        tree.title = category.categoryName;
                        tree.children = GetMateialName(helper, category);
                        listTree.Add(tree);
                    }
                }
                return listTree;
            }
        }

        private static List<Tree> GetMateialName(IDbInterface helper,Category category)
        {
            List<Tree> listTree2 = new List<Tree>();
            List<MaterialCode> materialcode = new List<MaterialCode>();
            foreach (var income in helper.Select<InCome>(p => p.categoryId == category.id))
            {
                Tree tree = new Tree();
                bool isSame = true;
                MaterialCode materialCode = helper.FindId<MaterialCode>(income.codeId);
                foreach (var mc in materialcode)
                {
                    if (mc.materialNameId == materialCode.materialNameId)
                    {
                        isSame = false;
                        continue;
                    }
                }
                if (isSame)
                {
                    materialcode.Add(materialCode);
                    int materialNameId = materialCode.materialNameId;
                    tree.title = helper.FindId<Name>(materialNameId).materialName;
                    tree.children = GetModelName(materialNameId, helper);
                    listTree2.Add(tree);
                }
            }
            return listTree2;
        }

        private static List<Tree> GetModelName(int materialNameId, IDbInterface helper)
        {
            List<Tree> listTree3 = new List<Tree>();
            foreach (var code in helper.Select<MaterialCode>(p => p.materialNameId == materialNameId))
            {
                Tree tree = new Tree();
                tree.title = helper.FindId<Model>(code.materialModelId).modelName;
                tree.children = null;
                listTree3.Add(tree);
            }
            return listTree3;
        }
    }
}
