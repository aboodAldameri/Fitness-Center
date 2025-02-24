﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Center.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Gymtext> Gymtexts { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Pagecontent> Pagecontents { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Payment2> Payment2s { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subsic> Subsics { get; set; }

    public virtual DbSet<Subsicription> Subsicriptions { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.100.14)(PORT=1521)(CONNECT_DATA=(SID=orcl))));User Id=C##ABDULLAH1; Password=Test321;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##ABDULLAH1")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008343");

            entity.ToTable("CUSTOMER");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.EmployeeId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.Fname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FNAME");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH");
            entity.Property(e => e.Lname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LNAME");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.SubscriptionId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SUBSCRIPTION_ID");

            entity.HasOne(d => d.Employee).WithMany(p => p.Customers)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_EMPLOYEE_ID");

            entity.HasOne(d => d.Role).WithMany(p => p.Customers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ROLE_ID1");

            entity.HasOne(d => d.Subscription).WithMany(p => p.Customers)
                .HasForeignKey(d => d.SubscriptionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_SUBSCRIPTION_ID");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008340");

            entity.ToTable("EMPLOYEES");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Fname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FNAME");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH");
            entity.Property(e => e.Lname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LNAME");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLE_ID");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ROLE_ID");
        });

        modelBuilder.Entity<Gymtext>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008381");

            entity.ToTable("GYMTEXT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Content)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("CONTENT");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008350");

            entity.ToTable("LOGIN");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.Role).WithMany(p => p.Logins)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ROLE_ID2");
        });

        modelBuilder.Entity<Pagecontent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008385");

            entity.ToTable("PAGECONTENT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Imageurl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGEURL");
            entity.Property(e => e.Sectionname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SECTIONNAME");
            entity.Property(e => e.Subtitle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SUBTITLE");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008352");

            entity.ToTable("PAYMENT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Bankname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("BANKNAME");
            entity.Property(e => e.Iban)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IBAN");
            entity.Property(e => e.Total)
                .HasColumnType("NUMBER")
                .HasColumnName("TOTAL");
            entity.Property(e => e.Useremail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USEREMAIL");
        });

        modelBuilder.Entity<Payment2>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008371");

            entity.ToTable("PAYMENT2");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Bankname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("BANKNAME");
            entity.Property(e => e.Iban)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IBAN");
            entity.Property(e => e.Subscriptionid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SUBSCRIPTIONID");
            entity.Property(e => e.Total)
                .HasColumnType("NUMBER")
                .HasColumnName("TOTAL");

            entity.HasOne(d => d.Subscription).WithMany(p => p.Payment2s)
                .HasForeignKey(d => d.Subscriptionid)
                .HasConstraintName("FK_PAYMENT_SUBSCRIPTION");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008354");

            entity.ToTable("PLANS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Planname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PLANNAME");
            entity.Property(e => e.Price)
                .HasColumnType("NUMBER")
                .HasColumnName("PRICE");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008338");

            entity.ToTable("ROLES");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ROLE_NAME");
        });

        modelBuilder.Entity<Subsic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008367");

            entity.ToTable("SUBSIC");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Enddate)
                .HasColumnType("DATE")
                .HasColumnName("ENDDATE");
            entity.Property(e => e.NumOfMonths)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("NUM_OF_MONTHS");
            entity.Property(e => e.Planid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PLANID");
            entity.Property(e => e.Startdate)
                .HasColumnType("DATE")
                .HasColumnName("STARTDATE");
            entity.Property(e => e.Totalamount)
                .HasColumnType("NUMBER")
                .HasColumnName("TOTALAMOUNT");

            entity.HasOne(d => d.Plan).WithMany(p => p.Subsics)
                .HasForeignKey(d => d.Planid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_PLANID");
        });

        modelBuilder.Entity<Subsicription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008336");

            entity.ToTable("SUBSICRIPTION");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.NumOfMonths)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("NUM_OF_MONTHS");
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008356");

            entity.ToTable("TESTIMONIALS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Customername)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CUSTOMERNAME");
            entity.Property(e => e.Status)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("STATUS");
            entity.Property(e => e.Testimonial1)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("TESTIMONIAL");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008377");

            entity.ToTable("USERS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Fname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FNAME");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH");
            entity.Property(e => e.Lname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LNAME");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Roleid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLEID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USERNAME");
            entity.Property(e => e.Usertype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USERTYPE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
