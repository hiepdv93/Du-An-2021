﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NTSPRODUCT.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NTSWEBEntities : DbContext
    {
        public NTSWEBEntities()
            : base("name=NTSWEBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Adv> Advs { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<email> emails { get; set; }
        public virtual DbSet<GroupPartner> GroupPartners { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Lib> Libs { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<ProFaq> ProFaqs { get; set; }
        public virtual DbSet<SayWe> SayWes { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<Support> Supports { get; set; }
        public virtual DbSet<WhyChooseUss> WhyChooseUsses { get; set; }
        public virtual DbSet<Faq> Faqs { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Config> Configs { get; set; }
        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<LichSuTruyCap> LichSuTruyCaps { get; set; }
    }
}
