namespace Shop_Shoes.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Data_Shoes : DbContext
    {
        public Data_Shoes()
            : base("name=Data_Shoes")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Order_s> Order_s { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Style> Styles { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Type_s> Type_s { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .Property(e => e.Color)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Order_s>()
                .Property(e => e.Shipmobile)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Order_s>()
                .Property(e => e.ShipEmail)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Type_s>()
                .HasMany(e => e.Items)
                .WithOptional(e => e.Type_s)
                .HasForeignKey(e => e.Id_Type);
        }
    }
}
