using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LegoProdavnica.Models;

public partial class LegoProdavnicaContext : DbContext
{
    public LegoProdavnicaContext()
    {
    }

    public LegoProdavnicaContext(DbContextOptions<LegoProdavnicaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Narudzbina> Narudzbinas { get; set; }

    public virtual DbSet<Profil> Profils { get; set; }

    public virtual DbSet<Proizvod> Proizvods { get; set; }

    public virtual DbSet<Racun> Racuns { get; set; }

    public virtual DbSet<RacunProizvod> RacunProizvods { get; set; }

    public virtual DbSet<Recenzija> Recenzijas { get; set; }

    public virtual DbSet<Rezervacija> Rezervacijas { get; set; }

    public virtual DbSet<Uloga> Ulogas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=JOVAN-DESKTOP;DataBase=LegoProdavnica;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Narudzbina>(entity =>
        {
            entity.HasKey(e => e.NarudzbinaId).HasName("PK__Narudzbi__282329EF1DF5C673");

            entity.ToTable("Narudzbina");

            entity.Property(e => e.NarudzbinaId).HasColumnName("NarudzbinaID");
            entity.Property(e => e.Adresa).HasColumnType("datetime");
            entity.Property(e => e.DatumDostave).HasColumnType("datetime");
            entity.Property(e => e.DatumKreacije).HasColumnType("datetime");
            entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");
            entity.Property(e => e.ProizvodId).HasColumnName("ProizvodID");

            entity.HasOne(d => d.Korisnik).WithMany(p => p.Narudzbinas)
                .HasForeignKey(d => d.KorisnikId)
                .HasConstraintName("FK__Narudzbin__Koris__398D8EEE");

            entity.HasOne(d => d.Proizvod).WithMany(p => p.Narudzbinas)
                .HasForeignKey(d => d.ProizvodId)
                .HasConstraintName("FK__Narudzbin__Proiz__38996AB5");
        });

        modelBuilder.Entity<Profil>(entity =>
        {
            entity.HasKey(e => e.ProfilId).HasName("PK__Profil__5E0A2D9DCE216678");

            entity.ToTable("Profil");

            entity.Property(e => e.ProfilId).HasColumnName("ProfilID");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Ime)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.KorisnickoIme)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Prezime)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Sifra)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UlogaId).HasColumnName("UlogaID");

            entity.HasOne(d => d.Uloga).WithMany(p => p.Profils)
                .HasForeignKey(d => d.UlogaId)
                .HasConstraintName("FK__Profil__Email__2A4B4B5E");
        });

        modelBuilder.Entity<Proizvod>(entity =>
        {
            entity.HasKey(e => e.ProizvodId).HasName("PK__Proizvod__21A8BE18D7552233");

            entity.ToTable("Proizvod");

            entity.Property(e => e.ProizvodId).HasColumnName("ProizvodID");
            entity.Property(e => e.AlteranteId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("AlteranteID");
            entity.Property(e => e.Cena).HasColumnType("money");
            entity.Property(e => e.Godine)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Naziv)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Pakovanje)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Slika)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Tip)
                .HasMaxLength(90)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Racun>(entity =>
        {
            entity.HasKey(e => e.RacunId).HasName("PK__Racun__07B8F7ACDBB4785F");

            entity.ToTable("Racun");

            entity.Property(e => e.RacunId).HasColumnName("RacunID");
            entity.Property(e => e.AlternateId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("AlternateID");
            entity.Property(e => e.DatumIzdavanja).HasColumnType("datetime");
            entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");
            entity.Property(e => e.RadnikId).HasColumnName("RadnikID");
            entity.Property(e => e.UkupnaCena).HasColumnType("money");

            entity.HasOne(d => d.Korisnik).WithMany(p => p.RacunKorisniks)
                .HasForeignKey(d => d.KorisnikId)
                .HasConstraintName("FK__Racun__KorisnikI__2E1BDC42");

            entity.HasOne(d => d.Radnik).WithMany(p => p.RacunRadniks)
                .HasForeignKey(d => d.RadnikId)
                .HasConstraintName("FK__Racun__RadnikID__2D27B809");
        });

        modelBuilder.Entity<RacunProizvod>(entity =>
        {
            entity.HasKey(e => new { e.RacunId, e.ProizvodId }).HasName("PK__Racun_Pr__95A27C4D13A38219");

            entity.ToTable("Racun_Proizvod");

            entity.Property(e => e.RacunId).HasColumnName("RacunID");
            entity.Property(e => e.ProizvodId).HasColumnName("ProizvodID");
            entity.Property(e => e.DatumDodavanja).HasColumnType("datetime");

            entity.HasOne(d => d.Proizvod).WithMany(p => p.RacunProizvods)
                .HasForeignKey(d => d.ProizvodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Racun_Pro__Proiz__31EC6D26");

            entity.HasOne(d => d.Racun).WithMany(p => p.RacunProizvods)
                .HasForeignKey(d => d.RacunId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Racun_Pro__Racun__30F848ED");
        });

        modelBuilder.Entity<Recenzija>(entity =>
        {
            entity.HasKey(e => e.RecenzijaId).HasName("PK__Recenzij__D36C6090D7C5D72E");

            entity.ToTable("Recenzija");

            entity.Property(e => e.RecenzijaId).HasColumnName("RecenzijaID");
            entity.Property(e => e.AlternateId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("AlternateID");
            entity.Property(e => e.DatumKreacija).HasColumnType("datetime");
            entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");
            entity.Property(e => e.ProizvodId).HasColumnName("ProizvodID");

            entity.HasOne(d => d.Korisnik).WithMany(p => p.Recenzijas)
                .HasForeignKey(d => d.KorisnikId)
                .HasConstraintName("FK__Recenzija__Koris__3D5E1FD2");

            entity.HasOne(d => d.Proizvod).WithMany(p => p.Recenzijas)
                .HasForeignKey(d => d.ProizvodId)
                .HasConstraintName("FK__Recenzija__Proiz__3E52440B");
        });

        modelBuilder.Entity<Rezervacija>(entity =>
        {
            entity.HasKey(e => e.RezervacijaId).HasName("PK__Rezervac__CABA44FD67B5F96F");

            entity.ToTable("Rezervacija");

            entity.Property(e => e.RezervacijaId).HasColumnName("RezervacijaID");
            entity.Property(e => e.DatumDostave).HasColumnType("datetime");
            entity.Property(e => e.DatumKreacije).HasColumnType("datetime");
            entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");
            entity.Property(e => e.ProizvodId).HasColumnName("ProizvodID");

            entity.HasOne(d => d.Korisnik).WithMany(p => p.Rezervacijas)
                .HasForeignKey(d => d.KorisnikId)
                .HasConstraintName("FK__Rezervaci__Koris__35BCFE0A");

            entity.HasOne(d => d.Proizvod).WithMany(p => p.Rezervacijas)
                .HasForeignKey(d => d.ProizvodId)
                .HasConstraintName("FK__Rezervaci__Proiz__34C8D9D1");
        });

        modelBuilder.Entity<Uloga>(entity =>
        {
            entity.HasKey(e => e.UlogaId).HasName("PK__Uloga__DCAB23EB48DE9A30");

            entity.ToTable("Uloga");

            entity.Property(e => e.UlogaId).HasColumnName("UlogaID");
            entity.Property(e => e.Opis)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Tip)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
