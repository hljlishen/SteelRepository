using DbInterface;
using DbService;
using System;
using System.Collections.Generic;

namespace Models.Tests
{
    class DataGenerator
    {
        public List<Name> names = new List<Name>();
        public List<Model> models = new List<Model>();
        public List<MaterialCode> codes = new List<MaterialCode>();
        public List<InCome> incomes = new List<InCome>();
        public List<RecheckReport> reports = new List<RecheckReport>();
        public List<OutCome> outcomes = new List<OutCome>();

        public void SetupReports()
        {
            reports.Add(new RecheckReport() { incomeId = incomes[0].id, recheckTime = DateTime.Now.AddDays(-50) });
            reports.Add(new RecheckReport() { incomeId = incomes[0].id, recheckTime = DateTime.Now.AddDays(-370) });
            reports.Add(new RecheckReport() { incomeId = incomes[0].id, recheckTime = DateTime.Now.AddDays(-736) });

            reports.Add(new RecheckReport() { incomeId = incomes[1].id, recheckTime = DateTime.Now.AddDays(-138) });
            reports.Add(new RecheckReport() { incomeId = incomes[1].id, recheckTime = DateTime.Now.AddDays(-557) });
            reports.Add(new RecheckReport() { incomeId = incomes[1].id, recheckTime = DateTime.Now.AddDays(-936) });

            reports.Add(new RecheckReport() { incomeId = incomes[2].id, recheckTime = DateTime.Now.AddDays(-80) });
            reports.Add(new RecheckReport() { incomeId = incomes[2].id, recheckTime = DateTime.Now.AddDays(-267) });
            reports.Add(new RecheckReport() { incomeId = incomes[2].id, recheckTime = DateTime.Now.AddDays(-400) });

            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                helper.InsertRange(reports);
            }
        }

        private void SetupOutCome()
        {

        }

        public void SetupMaterialCode()
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
                    //var code = new MaterialCode() { materialModelId = models[i].id, materialNameId = names[i].id, code = $"C{i + 1}" };
                    //codes.Add(code);
                    //helper.Insert(code);
                    codes.Add(MaterialCode.Insert($"C{i + 1}", $"N{i + 1}", $"M{i + 1}", helper));
                }
            }
        }

        public void SetupIncome()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {

                for (int i = 0; i < 3; i++)
                {
                    var income = InCome.NewInCome(DateTime.Now, 1, $"C{i + 1}", $"N{i + 1}", $"M{i + 1}", $"B{i + 1}", 1, "千克", 50, 1, 10, "KG", 1, null);
                    incomes.Add(income);
                }

                var inc = InCome.NewInCome(DateTime.Now, 1, $"C2", $"N2", $"M2", $"B4", 1, "千克", 50, 1, 10, "KG", 1, null);
                inc.storageTime = DateTime.Now.AddDays(-10);
                helper.Update(inc);
                incomes.Add(inc);
            }
        }

        public void SetupData()
        {
            SetupMaterialCode();
            SetupIncome();
            SetupReports();
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
                helper.DeleteRange(reports);
            }
        }
    }
}
