using System;
using System.Collections.Generic;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public partial class TenantBridgeContext : DbContext
{
    public TenantBridgeContext()
    {
    }

    public TenantBridgeContext(DbContextOptions<TenantBridgeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Lease> Leases { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<Reminder> Reminders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-416H4TC8;Initial Catalog=Tenant_Bridge;TrustServerCertificate=True;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lease>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true, "DF_Leases_active")
                .HasColumnName("active");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())", "DF_Leases_created_at")
                .HasColumnName("created_at");
            entity.Property(e => e.DepositAmount).HasColumnName("deposit_amount");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.LandlordId).HasColumnName("landlord_id");
            entity.Property(e => e.PropertyId).HasColumnName("property_id");
            entity.Property(e => e.RentAmount).HasColumnName("rent_amount");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TenantId).HasColumnName("tenant_id");

            entity.HasOne(d => d.Landlord).WithMany(p => p.LeaseLandlords)
                .HasForeignKey(d => d.LandlordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Leases_Landlord_Id");

            entity.HasOne(d => d.Property).WithMany(p => p.Leases)
                .HasForeignKey(d => d.PropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Leases_Property_Id");

            entity.HasOne(d => d.Tenant).WithMany(p => p.LeaseTenants)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Leases_Tenant_Id");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())", "DF_Payments_created_at")
                .HasColumnName("created_at");
            entity.Property(e => e.LeaseId).HasColumnName("lease_id");
            entity.Property(e => e.PaidDate).HasColumnName("paid_date");
            entity.Property(e => e.PropertyId).HasColumnName("property_id");
            entity.Property(e => e.ReceiptNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("receipt_no");
            entity.Property(e => e.RentMonth)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rent_month");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type");

            entity.HasOne(d => d.Lease).WithMany(p => p.Payments)
                .HasForeignKey(d => d.LeaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Lease_Id");

            entity.HasOne(d => d.Property).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Property_id");
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true, "DF_Properties_active")
                .HasColumnName("active");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())", "DF_Properties_createdAt")
                .HasColumnName("created_at");
            entity.Property(e => e.LandlordId).HasColumnName("landlord_id");

            entity.HasOne(d => d.Landlord).WithMany(p => p.Properties)
                .HasForeignKey(d => d.LandlordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Properties_Landlord");
        });

        modelBuilder.Entity<Reminder>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Message)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.ReceivedBy).HasColumnName("received_by");
            entity.Property(e => e.SentBy).HasColumnName("sent_by");

            entity.HasOne(d => d.ReceivedByNavigation).WithMany(p => p.ReminderReceivedByNavigations)
                .HasForeignKey(d => d.ReceivedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reminders_Received_By");

            entity.HasOne(d => d.SentByNavigation).WithMany(p => p.ReminderSentByNavigations)
                .HasForeignKey(d => d.SentBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reminders_Sent_By");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
