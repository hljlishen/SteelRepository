using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Tests
{
    [TestClass()]
    public class MaterialCodeTests
    {
        private List<Name> names = new List<Name>();
        private List<Model> models = new List<Model>();
        private List<MaterialCode> codes = new List<MaterialCode>();

        private void SetupData()
        {
            names.Add(new Name() { materialName = "#1" });
            names.Add(new Name() { materialName = "#2" });
            names.Add(new Name() { materialName = "#3" });

            models.Add(new Model() { modelName = "*1" });
            models.Add(new Model() { modelName = "*2" });
            models.Add(new Model() { modelName = "*3" });
        }
        [TestMethod()]
        public void GetNameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetModelTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InsertTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetMaterialCodeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetMaterialCodeTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InsertTest1()
        {
            Assert.Fail();
        }
    }
}