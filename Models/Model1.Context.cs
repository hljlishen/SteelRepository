﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SteelRepositoryDbEntities : DbContext
    {
        public SteelRepositoryDbEntities()
            : base("name=SteelRepositoryDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<InCome> InCome { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<MaterialCode> MaterialCode { get; set; }
        public virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<Name> Name { get; set; }
        public virtual DbSet<OutCome> OutCome { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<RecheckReport> RecheckReport { get; set; }
    }
}
