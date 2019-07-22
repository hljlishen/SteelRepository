using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SteelRepository.Models
{
    public class SteelRepositoryDB : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<InCome> InComes { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Manufacturers> Manufacturers { get; set; }
        public DbSet<MaterialCode> MaterialCodes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Name> Names { get; set; }
        public DbSet<OutCome> OutComes { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<RecheckReport> RecheckReports { get; set; }
    }
}