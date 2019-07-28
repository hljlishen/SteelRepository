using DbInterface;
using DbService;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Tests
{
    class DataGenerator
    {
        public List<Name> names = new List<Name>();
        public List<Model> models = new List<Model>();
        public List<MaterialCode> codes = new List<MaterialCode>();
        public List<InCome> incomes = new List<InCome>();

        public void SetupData()
        {
            names.Add(new Name() { materialName = "N1" });
            names.Add(new Name() { materialName = "N2" });
            names.Add(new Name() { materialName = "N3" });

            models.Add(new Model() { modelName = "M1" });
            models.Add(new Model() { modelName = "M2" });
            models.Add(new Model() { modelName = "M3" });

            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                helper.InsertRange(names);

                helper.InsertRange(models);

                for (int i = 0; i < 3; i++)
                {
                    var code = new MaterialCode() { materialModelId = models[i].id, materialNameId = names[i].id, code = $"C{i + 1}" };
                    codes.Add(code);
                    helper.Insert(code);
                }

                for (int i = 0; i < 3; i++)
                {
                    var income = InCome.NewInCome(1, $"C{i+1}", $"N{i+1}", $"M{i+1}", $"B{i+1}", 1, "千克", 50, 1, 10, "KG", 1, null);
                    incomes.Add(income);
                }
            }
        }

        public void DestroyData()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                helper.DeleteRange(models);
                helper.DeleteRange(names);
                helper.DeleteRange(codes);
                foreach (var item in incomes)
                {
                    helper.DeleteWhere<Inventory>(p => p.incomeId == item.id);
                }
                helper.DeleteRange(incomes);
            }
        }
    }
}
