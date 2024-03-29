﻿using DbInterface;
using DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class Manufacturer
    {
        public static Manufacturer GetManufacturer(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.FindId<Manufacturer>(id);
            }
        }
        public static int Insert(Manufacturer manufacturer)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                if (helper.Select<Manufacturer>(p => p.manufacturersName == manufacturer.manufacturersName).Count <= 0)
                    return helper.Insert(manufacturer);
                else
                    return 0;
            }
        }
        

        public static int Delete(int id)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                foreach (var income in helper.SelectAll<InCome>())
                {
                    if (income.menufactureId == id)
                        return 0;
                }
                return helper.Delete<Manufacturer>(id);
            }
        }
        public static List<Manufacturer> SelectAll()
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                return helper.SelectAll<Manufacturer>();
            }
        }
        public static int Update(Manufacturer manufacturer)
        {
            using (IDbInterface helper = new DbHelper(new SteelRepositoryDbEntities()))
            {
                if (helper.Select<Manufacturer>(p => p.manufacturersName == manufacturer.manufacturersName && p.id != manufacturer.id).Count <= 0)
                    return helper.Update(manufacturer);
                else
                    return 0;
            }
        }
    }
}
