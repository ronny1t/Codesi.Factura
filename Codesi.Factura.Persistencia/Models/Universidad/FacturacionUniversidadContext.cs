using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Models.Universidad;

public partial class FacturacionUniversidadContext : DbContext
{
    public FacturacionUniversidadContext()
    {
    }

    public FacturacionUniversidadContext(
        DbContextOptions<FacturacionUniversidadContext> options)
        : base(options)
    {
    }

    // =========================================================
    // TABLAS DEL SISTEMA
    // =========================================================

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FacturaDetalle> FacturaDetalles { get; set; }

    public virtual DbSet<FacturaPago> FacturaPagos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    // =========================================================
    // AUTENTICACIÓN Y AUTORIZACIÓN
    // =========================================================

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    // =========================================================
    // CONEXIÓN A SQL SERVER
    // =========================================================

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    /*
    #warning To protect potentially sensitive information in your connection string,
    you should move it out of source code.
    */
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=facturacion_universidad;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    // =========================================================
    // CONFIGURACIÓN DEL MODELO
    // =========================================================

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // =====================================================
        // CATEGORIAS
        // =====================================================

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria)
                .HasName("PK__categori__CD54BC5AB977FE91");

            entity.Property(e => e.Activo)
                .HasDefaultValue(true);
        });

        // =====================================================
        // CLIENTES
        // =====================================================

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente)
                .HasName("PK__clientes__677F38F509432EF8");

            entity.Property(e => e.Activo)
                .HasDefaultValue(true);

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())");
        });

        // =====================================================
        // FACTURAS
        // =====================================================

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura)
                .HasName("PK__facturas__6C08ED53FB6C9FC8");

            entity.Property(e => e.ClaveAcceso)
                .IsFixedLength();

            entity.Property(e => e.Establecimiento)
                .HasDefaultValue("001")
                .IsFixedLength();

            entity.Property(e => e.EstadoSri)
                .HasDefaultValue("CREADA");

            entity.Property(e => e.FechaEmision)
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.PuntoEmision)
                .HasDefaultValue("001")
                .IsFixedLength();

            entity.Property(e => e.Secuencial)
                .IsFixedLength();

            entity.HasOne(d => d.IdClienteNavigation)
                .WithMany(p => p.Facturas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(
                    "FK__facturas__id_cli__693CA210");
        });

        // =====================================================
        // FACTURA DETALLES
        // =====================================================

        modelBuilder.Entity<FacturaDetalle>(entity =>
        {
            entity.HasKey(e => e.IdDetalle)
                .HasName("PK__factura___4F1332DED71DF029");

            entity.HasOne(d => d.IdFacturaNavigation)
                .WithMany(p => p.FacturaDetalles)
                .HasConstraintName(
                    "FK__factura_d__id_fa__6FE99F9F");

            entity.HasOne(d => d.IdProductoNavigation)
                .WithMany(p => p.FacturaDetalles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(
                    "FK__factura_d__id_pr__70DDC3D8");
        });

        // =====================================================
        // FACTURA PAGOS
        // =====================================================

        modelBuilder.Entity<FacturaPago>(entity =>
        {
            entity.HasKey(e => e.IdPago)
                .HasName("PK__factura___0941B0748D2C9523");

            entity.HasOne(d => d.IdFacturaNavigation)
                .WithMany(p => p.FacturaPagos)
                .HasConstraintName(
                    "FK__factura_p__id_fa__74AE54BC");
        });

        // =====================================================
        // PRODUCTOS
        // =====================================================

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto)
                .HasName("PK__producto__FF341C0D1FF23EE5");

            entity.Property(e => e.Activo)
                .HasDefaultValue(true);

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.TarifaIva)
                .HasDefaultValue(15.00m);

            entity.HasOne(d => d.IdCategoriaNavigation)
                .WithMany(p => p.Productos)
                .HasConstraintName(
                    "FK__productos__id_ca__5629CD9C");
        });

        // =========================================================
        // ROLES
        // =========================================================

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.ToTable("Roles");

            entity.Property(e => e.Nombre)
                .IsRequired();

            entity.Property(e => e.Activo)
                .HasDefaultValue(true);
        });

        // =========================================================
        // USUARIOS
        // =========================================================

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("Usuarios");

            // -------------------------------------------------
            // ID MICROSOFT
            // -------------------------------------------------

            entity.Property(e => e.IdMicrosoft)
                .HasMaxLength(100)
                .IsRequired(false);

            // -------------------------------------------------
            // EMAIL
            // -------------------------------------------------

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsRequired();

            // -------------------------------------------------
            // NOMBRE
            // -------------------------------------------------

            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsRequired();

            // -------------------------------------------------
            // CEDULA
            // -------------------------------------------------

            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .IsRequired();

            // -------------------------------------------------
            // TIPO DE PERSONA
            //
            // DOCENTE
            // ESTUDIANTE
            // -------------------------------------------------

            entity.Property(e => e.TipoPersona)
                .HasMaxLength(20)
                .IsRequired();

            // -------------------------------------------------
            // ACTIVO
            // -------------------------------------------------

            entity.Property(e => e.Activo)
                .HasDefaultValue(true);

            // -------------------------------------------------
            // FECHA DE CREACIÓN
            // -------------------------------------------------

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(sysdatetime())");

            // -------------------------------------------------
            // ÍNDICE ÚNICO EMAIL
            // -------------------------------------------------

            entity.HasIndex(e => e.Email)
                .IsUnique()
                .HasDatabaseName("UX_Usuarios_Email");

            // -------------------------------------------------
            // ÍNDICE ÚNICO CÉDULA
            // -------------------------------------------------

            entity.HasIndex(e => e.Cedula)
                .IsUnique()
                .HasDatabaseName("UX_Usuarios_Cedula");
        });

        // =========================================================
        // USUARIOS <-> ROLES
        //
        // Relación muchos a muchos.
        //
        // Tablas:
        //
        // Usuarios
        // Roles
        // UsuarioRoles
        //
        // La tabla UsuarioRoles tiene:
        //
        // IdUsuario
        // IdRol
        // =========================================================

        modelBuilder.Entity<Usuarios>()
            .HasMany(e => e.IdRol)
            .WithMany(e => e.IdUsuario)
            .UsingEntity<Dictionary<string, object>>(
                "UsuarioRoles",

                // -------------------------------------------------
                // UsuarioRoles -> Roles
                // -------------------------------------------------

                right => right
                    .HasOne<Roles>()
                    .WithMany()
                    .HasForeignKey("IdRol")
                    .OnDelete(DeleteBehavior.Cascade),

                // -------------------------------------------------
                // UsuarioRoles -> Usuarios
                // -------------------------------------------------

                left => left
                    .HasOne<Usuarios>()
                    .WithMany()
                    .HasForeignKey("IdUsuario")
                    .OnDelete(DeleteBehavior.Cascade),

                // -------------------------------------------------
                // CONFIGURACIÓN DE LA TABLA INTERMEDIA
                // -------------------------------------------------

                join =>
                {
                    join.HasKey(
                        "IdUsuario",
                        "IdRol");

                    join.ToTable("UsuarioRoles");
                });

        // =========================================================
        // CONFIGURACIÓN PARCIAL
        // =========================================================

        OnModelCreatingPartial(modelBuilder);
    }

    // =========================================================
    // MÉTODO PARCIAL
    // =========================================================

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}