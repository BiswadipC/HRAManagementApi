using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models;

public partial class SampleContext : DbContext
{
    public SampleContext()
    {
    }

    public SampleContext(DbContextOptions<SampleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Designation> Designations { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeePayComponent> EmployeePayComponents { get; set; }

    public virtual DbSet<EmployeesAddress> EmployeesAddresses { get; set; }

    public virtual DbSet<EmployeesFamilyDetail> EmployeesFamilyDetails { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<ModulePolicyMapping> ModulePolicyMappings { get; set; }

    public virtual DbSet<PayComponent> PayComponents { get; set; }

    public virtual DbSet<PayScale> PayScales { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("server=Localhost\\SQLEXPRESS; database=sample; User Id=sa;Password=welcome@123; encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.IdNo).HasName("PK__Departme__B773C99903072F9F");

            entity.HasIndex(e => e.Name, "UQ__Departme__737584F6DCC41207").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Designation>(entity =>
        {
            entity.HasKey(e => e.IdNo).HasName("PK__Designat__B773C999FA188FB7");

            entity.HasIndex(e => e.Name, "UQ__Designat__737584F6C99ADB78").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdNo).HasName("PK__Employee__B773C999A540F7D7");

            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Salary).HasColumnType("decimal(20, 2)");

            entity.HasOne(d => d.DepartmentIdNoNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentIdNo)
                .HasConstraintName("FK__Employees__Depar__3B75D760");

            entity.HasOne(d => d.DesignationIdNoNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DesignationIdNo)
                .HasConstraintName("fk_Employees_DesignationIdNo");

            entity.HasOne(d => d.PayScaleIdNoNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PayScaleIdNo)
                .HasConstraintName("fk_Employees_PayScaleIdNo");
        });

        modelBuilder.Entity<EmployeePayComponent>(entity =>
        {
            entity.HasKey(e => e.IdNo).HasName("PK__Employee__B773C99915991ECE");

            entity.Property(e => e.OverrideFixedValue)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(20, 2)");
            entity.Property(e => e.OverridePercentageValue)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ValueType)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Component).WithMany(p => p.EmployeePayComponents)
                .HasForeignKey(d => d.ComponentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeP__Compo__01142BA1");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeePayComponents)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeP__Emplo__00200768");
        });

        modelBuilder.Entity<EmployeesAddress>(entity =>
        {
            entity.HasKey(e => e.IdNo).HasName("PK__Employee__B773C99949B9D335");

            entity.ToTable("EmployeesAddress");

            entity.Property(e => e.PermAddress)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PermCity)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PermCountry)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PermDistrict)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PermPin)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PermState)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PreAddress)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PreCity)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PreCountry)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PreDistrict)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PrePin)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PreState)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.EmployeeMainIdNoNavigation).WithMany(p => p.EmployeesAddresses)
                .HasForeignKey(d => d.EmployeeMainIdNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employees__Emplo__49C3F6B7");
        });

        modelBuilder.Entity<EmployeesFamilyDetail>(entity =>
        {
            entity.HasKey(e => e.IdNo).HasName("PK__Employee__B773C9994604BBFA");

            entity.Property(e => e.AadhaarNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MemberName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PanNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Relationship)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.EmployeeMainIdNoNavigation).WithMany(p => p.EmployeesFamilyDetails)
                .HasForeignKey(d => d.EmployeeMainIdNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employees__Emplo__4D94879B");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.ModuleName).HasName("PK__Modules__EAC9AEC28AF0A142");

            entity.Property(e => e.ModuleName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ModulePolicyMapping>(entity =>
        {
            entity.HasKey(e => e.IdNo).HasName("PK__ModulePo__B773C999446BCB8D");

            entity.Property(e => e.ModuleName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PermissionType)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PolicyName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PayComponent>(entity =>
        {
            entity.HasKey(e => e.IdNo).HasName("PK__PayCompo__B773C999595190B9");

            entity.HasIndex(e => e.ComponentName, "UQ__PayCompo__DB06D1C131776147").IsUnique();

            entity.Property(e => e.CalculationMethod)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ComponentName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FixedValue).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.IsActive)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValue("Yes");
            entity.Property(e => e.IsTaxable)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.PercentageValue).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Type)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PayScale>(entity =>
        {
            entity.HasKey(e => e.IdNo).HasName("PK__PayScale__B773C9997B5E3453");

            entity.HasIndex(e => e.PayScaleCode, "UQ__PayScale__07347B115AD19279").IsUnique();

            entity.Property(e => e.BasicSalary).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.ConveyanceAllowance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(20, 2)");
            entity.Property(e => e.DaPercentage)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(20, 2)")
                .HasColumnName("DA_Percentage");
            entity.Property(e => e.HraPercentage)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(20, 2)")
                .HasColumnName("HRA_Percentage");
            entity.Property(e => e.MedicalAllowance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(20, 2)");
            entity.Property(e => e.OtherFixedAllowances)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(20, 2)");
            entity.Property(e => e.PayScaleCode)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdNo).HasName("PK__Users__B773C999FC89639D");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4334AF8F8").IsUnique();

            entity.Property(e => e.IsAdmin)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
