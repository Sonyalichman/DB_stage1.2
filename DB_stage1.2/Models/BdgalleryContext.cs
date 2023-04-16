using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DB_stage1._2.Models;

public partial class BdgalleryContext : DbContext
{
    public BdgalleryContext()
    {
    }

    public BdgalleryContext(DbContextOptions<BdgalleryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Artist1> Artists1 { get; set; }

    public virtual DbSet<Exhibition> Exhibitions { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Guide> Guides { get; set; }

    public virtual DbSet<Painting> Paintings { get; set; }

    public virtual DbSet<PaintingsTechnique> PaintingsTechniques { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SeLTi;Database=BDgallery;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Artist__3214EC071EDED141");

            entity.ToTable("Artist");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.DateOfDeath).HasColumnType("date");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nickname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Artist1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Artists__3214EC079336E1AE");

            entity.ToTable("Artists");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.DateOfDeath).HasColumnType("date");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nickname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Exhibition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Exhibiti__3214EC07C2D6E44F");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Genre).WithMany(p => p.Exhibitions)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__Exhibitio__Genre__5070F446");

            entity.HasMany(d => d.Guides).WithMany(p => p.Exhibitions)
                .UsingEntity<Dictionary<string, object>>(
                    "ExhibitionGuide",
                    r => r.HasOne<Guide>().WithMany()
                        .HasForeignKey("GuideId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Exhibitio__Guide__571DF1D5"),
                    l => l.HasOne<Exhibition>().WithMany()
                        .HasForeignKey("ExhibitionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Exhibitio__Exhib__5629CD9C"),
                    j =>
                    {
                        j.HasKey("ExhibitionId", "GuideId").HasName("PK__Exhibiti__CCBA221B02DDEC5B");
                        j.ToTable("ExhibitionGuides");
                    });
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genre__3214EC07CBCD0B98");

            entity.ToTable("Genre");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Guide>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Guides__3214EC075E86BEC8");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ExhibitionId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Painting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Painting__3214EC07782234FC");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Artist)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreationEndDate).HasColumnType("date");
            entity.Property(e => e.CreationStartDate).HasColumnType("date");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Exhibition).WithMany(p => p.Paintings)
                .HasForeignKey(d => d.ExhibitionId)
                .HasConstraintName("FK__Paintings__Exhib__534D60F1");

            entity.HasMany(d => d.Artists).WithMany(p => p.Paintings)
                .UsingEntity<Dictionary<string, object>>(
                    "PaintingsArtist",
                    r => r.HasOne<Artist1>().WithMany()
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Paintings__Artis__5EBF139D"),
                    l => l.HasOne<Painting>().WithMany()
                        .HasForeignKey("PaintingId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Paintings__Paint__5DCAEF64"),
                    j =>
                    {
                        j.HasKey("PaintingId", "ArtistId").HasName("PK__Painting__DD7A9647572B9658");
                        j.ToTable("Paintings_Artists");
                    });

            entity.HasMany(d => d.Techniques).WithMany(p => p.Paintings)
                .UsingEntity<Dictionary<string, object>>(
                    "PaintingTechniqueRelation",
                    r => r.HasOne<PaintingsTechnique>().WithMany()
                        .HasForeignKey("TechniqueId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PaintingT__Techn__5AEE82B9"),
                    l => l.HasOne<Painting>().WithMany()
                        .HasForeignKey("PaintingId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PaintingT__Paint__59FA5E80"),
                    j =>
                    {
                        j.HasKey("PaintingId", "TechniqueId").HasName("PK__Painting__451B2743CDA7E2D7");
                        j.ToTable("PaintingTechniqueRelations");
                    });
        });

        modelBuilder.Entity<PaintingsTechnique>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Painting__3214EC07A7B0CA1E");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
