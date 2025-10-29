using System;
using System.Collections.Generic;
using FreelanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FreelanceApp.Data;

public partial class FreelanceContext : DbContext
{
    public FreelanceContext()
    {
    }

    public FreelanceContext(DbContextOptions<FreelanceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bid> Bids { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Freelancer> Freelancers { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<VActiveProject> VActiveProjects { get; set; }

    public virtual DbSet<VClientProjectHistory> VClientProjectHistories { get; set; }

    public virtual DbSet<VFreelancersWithRating> VFreelancersWithRatings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=db28102.public.databaseasp.net; Database=db28102; User Id=db28102; Password=4p?F=Hd3Te5+; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Cyrillic_General_CS_AS_KS_WS");

        modelBuilder.Entity<Bid>(entity =>
        {
            entity.HasKey(e => e.BidId).HasName("PK__Bids__4A733DB2500C214B");

            entity.Property(e => e.BidId).HasColumnName("BidID");
            entity.Property(e => e.BidAmount).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.FreelancerId).HasColumnName("FreelancerID");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Freelancer).WithMany(p => p.Bids)
                .HasForeignKey(d => d.FreelancerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bids__Freelancer__19DFD96B");

            entity.HasOne(d => d.Project).WithMany(p => p.Bids)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bids__ProjectID__18EBB532");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2BEDD7522A");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clients__E67E1A0454BD1917");

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ContactInfo).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Freelancer>(entity =>
        {
            entity.HasKey(e => e.FreelancerId).HasName("PK__Freelanc__3D00E30C50493DF0");

            entity.Property(e => e.FreelancerId).HasColumnName("FreelancerID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Specialization).HasMaxLength(255);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Projects__761ABED05910BA63");

            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.Budget).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Category).WithMany(p => p.Projects)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Projects__Catego__14270015");

            entity.HasOne(d => d.Client).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Projects__Client__160F4887");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AE7BA04A74");

            entity.HasIndex(e => e.ProjectId, "UQ__Reviews__761ABED1E24CCB76").IsUnique();

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.FreelancerId).HasColumnName("FreelancerID");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

            entity.HasOne(d => d.Client).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__ClientI__1F98B2C1");

            entity.HasOne(d => d.Freelancer).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.FreelancerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__Freelan__208CD6FA");

            entity.HasOne(d => d.Project).WithOne(p => p.Review)
                .HasForeignKey<Review>(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__Project__1EA48E88");
        });

        modelBuilder.Entity<VActiveProject>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_ActiveProjects");

            entity.Property(e => e.Budget).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.ClientName).HasMaxLength(255);
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<VClientProjectHistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_ClientProjectHistory");

            entity.Property(e => e.Budget).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ClientName).HasMaxLength(255);
            entity.Property(e => e.HiredFreelancer).HasMaxLength(255);
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.ProjectStatus).HasMaxLength(50);
            entity.Property(e => e.ProjectTitle).HasMaxLength(255);
        });

        modelBuilder.Entity<VFreelancersWithRating>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_FreelancersWithRating");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FreelancerId).HasColumnName("FreelancerID");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Specialization).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
