﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalMelkBin.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TheFinalMelkobinEntities : DbContext
    {
        public TheFinalMelkobinEntities()
            : base("name=TheFinalMelkobinEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<buildDate> buildDates { get; set; }
        public virtual DbSet<Categori> Categoris { get; set; }
        public virtual DbSet<city> cities { get; set; }
        public virtual DbSet<equipmentCategori> equipmentCategoris { get; set; }
        public virtual DbSet<equipmentPicture> equipmentPictures { get; set; }
        public virtual DbSet<equipmentSubCategori> equipmentSubCategoris { get; set; }
        public virtual DbSet<equipmetAcceptUser> equipmetAcceptUsers { get; set; }
        public virtual DbSet<equipmetUser> equipmetUsers { get; set; }
        public virtual DbSet<jobAcceptUser> jobAcceptUsers { get; set; }
        public virtual DbSet<jobCategori> jobCategoris { get; set; }
        public virtual DbSet<jobPicture> jobPictures { get; set; }
        public virtual DbSet<jobSubCategori> jobSubCategoris { get; set; }
        public virtual DbSet<jobUser> jobUsers { get; set; }
        public virtual DbSet<melkAcceptUser> melkAcceptUsers { get; set; }
        public virtual DbSet<picture> pictures { get; set; }
        public virtual DbSet<roll> rolls { get; set; }
        public virtual DbSet<SubCategori> SubCategoris { get; set; }
        public virtual DbSet<subCity> subCities { get; set; }
        public virtual DbSet<AdminUser> AdminUsers { get; set; }
        public virtual DbSet<RollAdmin> RollAdmins { get; set; }
        public virtual DbSet<childjobSubCategori> childjobSubCategoris { get; set; }
        public virtual DbSet<ChildSubCategori> ChildSubCategoris { get; set; }
        public virtual DbSet<childSubEquipmentCategori> childSubEquipmentCategoris { get; set; }
        public virtual DbSet<equipment> equipments { get; set; }
        public virtual DbSet<job> jobs { get; set; }
        public virtual DbSet<melk> melks { get; set; }
        public virtual DbSet<user> users { get; set; }
    }
}