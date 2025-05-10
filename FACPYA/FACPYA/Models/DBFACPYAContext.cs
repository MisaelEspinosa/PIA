using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FACPYA.Models;

public partial class DbfacpyaContext : DbContext
{
    public DbfacpyaContext()
    {
    }

    public DbfacpyaContext(DbContextOptions<DbfacpyaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Pais> Pais { get; set; }

    public virtual DbSet<PaqueteViaje> PaqueteViajes { get; set; }

    public virtual DbSet<Reservacion> Reservacions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Dbfacpya"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__D59466422326B18D");

            entity.ToTable("Cliente");

            entity.Property(e => e.Correo)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.PaisOrigen).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.PaisOrigenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cliente__PaisOri__398D8EEE");
        });

        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasKey(e => e.IdPais).HasName("PK__Pais__FC850A7B056D7245");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PaqueteViaje>(entity =>
        {
            entity.HasKey(e => e.IdPaquete).HasName("PK__PaqueteV__DE278F8B32913737");

            entity.ToTable("PaqueteViaje");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.DestinoPais).WithMany(p => p.PaqueteViajes)
                .HasForeignKey(d => d.DestinoPaisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaqueteVi__Desti__3C69FB99");
        });

        modelBuilder.Entity<Reservacion>(entity =>
        {
            entity.HasKey(e => e.IdReservacion).HasName("PK__Reservac__528246376BDDAB50");

            entity.ToTable("Reservacion");

            entity.Property(e => e.FechaReservacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Reservacions)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservaci__Clien__403A8C7D");

            entity.HasOne(d => d.Paquete).WithMany(p => p.Reservacions)
                .HasForeignKey(d => d.PaqueteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservaci__Paque__412EB0B6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
