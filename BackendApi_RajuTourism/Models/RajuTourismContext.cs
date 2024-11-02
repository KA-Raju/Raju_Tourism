using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BackendApi_RajuTourism.Models;

public partial class RajuTourismContext : DbContext
{
    public RajuTourismContext()
    {
    }

    public RajuTourismContext(DbContextOptions<RajuTourismContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Enquiry> Enquirys { get; set; }

    public virtual DbSet<Pack> Packs { get; set; }

    public virtual DbSet<RegisterDetail> RegisterDetails { get; set; }

    public virtual DbSet<UserPersonalDetail> UserPersonalDetails { get; set; }

   /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RajuTourism");
*/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Enquiry>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__tmp_ms_x__A9D105354220D6EF");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SpecialNote).IsUnicode(false);
            entity.Property(e => e.TravelDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Pack>(entity =>
        {
            entity.HasKey(e => e.PackId).HasName("PK__Packs__FA67656903802D17");

            entity.Property(e => e.PackId).ValueGeneratedNever();
            entity.Property(e => e.PackName).HasMaxLength(50);
        });

        modelBuilder.Entity<RegisterDetail>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Register__A9D10535B73B36E6");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        modelBuilder.Entity<UserPersonalDetail>(entity =>
        {
            entity.HasKey(e => e.Uemail).HasName("PK__UserPers__75B1691F0339644B");

            entity.Property(e => e.Uemail)
                .HasMaxLength(50)
                .HasColumnName("UEmail");
            entity.Property(e => e.UmobileNo).HasColumnName("UMobileNo");
            entity.Property(e => e.Uname)
                .HasMaxLength(50)
                .HasColumnName("UName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
