﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMS_V4
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class emsEntities : DbContext
    {
        public emsEntities()
            : base("name=emsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<tbl_Admin> tbl_Admin { get; set; }
        public DbSet<tbl_CastReligion> tbl_CastReligion { get; set; }
        public DbSet<tbl_ClassLevel> tbl_ClassLevel { get; set; }
        public DbSet<tbl_ContactDetail> tbl_ContactDetail { get; set; }
        public DbSet<tbl_Course> tbl_Course { get; set; }
        public DbSet<tbl_Employee> tbl_Employee { get; set; }
        public DbSet<tbl_EmploymentHistory> tbl_EmploymentHistory { get; set; }
        public DbSet<tbl_language> tbl_language { get; set; }
        public DbSet<tbl_MasterFee> tbl_MasterFee { get; set; }
        public DbSet<tbl_Nationality> tbl_Nationality { get; set; }
        public DbSet<tbl_ParentDetail> tbl_ParentDetail { get; set; }
        public DbSet<tbl_Salary> tbl_Salary { get; set; }
        public DbSet<tbl_Student> tbl_Student { get; set; }
        public DbSet<tbl_StudentResult> tbl_StudentResult { get; set; }
        public DbSet<tbl_Transport> tbl_Transport { get; set; }
        public DbSet<tbl_YearAcademy> tbl_YearAcademy { get; set; }
        public DbSet<tbl_Account> tbl_Account { get; set; }
    }
}
