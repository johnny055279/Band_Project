﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Band_Web.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BandProjectEntities : DbContext
    {
        public BandProjectEntities()
            : base("name=BandProjectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tBandFormat> tBandFormat { get; set; }
        public virtual DbSet<tDestination> tDestination { get; set; }
        public virtual DbSet<tMember> tMember { get; set; }
        public virtual DbSet<tOrder> tOrder { get; set; }
        public virtual DbSet<tOrderDetail> tOrderDetail { get; set; }
        public virtual DbSet<tProduct> tProduct { get; set; }
        public virtual DbSet<tProductDetail> tProductDetail { get; set; }
        public virtual DbSet<tProductType> tProductType { get; set; }
        public virtual DbSet<tShipStatus> tShipStatus { get; set; }
        public virtual DbSet<tTicketDetail> tTicketDetail { get; set; }
        public virtual DbSet<tTicketPurchase> tTicketPurchase { get; set; }
        public virtual DbSet<tUser> tUser { get; set; }
    }
}
