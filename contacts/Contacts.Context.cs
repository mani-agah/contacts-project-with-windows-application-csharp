﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace contacts
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ContactEntities1 : DbContext
    {
        public ContactEntities1()
            : base("name=ContactEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CityTbl> CityTbls { get; set; }
        public virtual DbSet<CountryTbl> CountryTbls { get; set; }
        public virtual DbSet<JobTbl> JobTbls { get; set; }
        public virtual DbSet<MyContactTbl> MyContactTbls { get; set; }
        public virtual DbSet<StateTbl> StateTbls { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
