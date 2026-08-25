using Microsoft.EntityFrameworkCore;
using Domain.Model;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }

        public TPIContext(DbContextOptions<TPIContext> options) : base(options)
        {
            //this.Database.EnsureCreated();
            //SeedInitialData();
        }

        internal TPIContext()
        {
            //this.Database.EnsureCreated();
            //SeedInitialData();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Evita que EF Core mapee estas clases arrastradas por navegación
            modelBuilder.Ignore<Turno>();
            modelBuilder.Ignore<ConsultaMedica>();
            modelBuilder.Ignore<Factura>();
            modelBuilder.Ignore<HistoriaClinica>();
            modelBuilder.Ignore<Administrativo>();

            modelBuilder.Entity<Persona>(entity =>
                {
                    entity.HasKey(p => p.Id);
                    entity.Property(p => p.Nombre)
                        .IsRequired()
                        .HasMaxLength(100);
                    entity.Property(p => p.Apellido)
                        .IsRequired()
                        .HasMaxLength(100);
                    entity.Property(p => p.TipoDocumento)
                        .IsRequired()
                        .HasMaxLength(20);
                    entity.Property(p => p.NroDocumento)
                        .IsRequired()
                        .HasMaxLength(20);
                    entity.Property(p => p.Email)
                        .IsRequired()
                        .HasMaxLength(100);
                    entity.Property(p => p.Telefono)
                        .IsRequired(false)
                        .HasMaxLength(20);
                });

            modelBuilder.Entity<Profesional>(entity =>
            {
                entity.ToTable("Profesionales");
                entity.Property(p => p.Matricula)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.HasOne(p => p._especialidad)
                    .WithMany()
                    .HasForeignKey(p => p._especialidadId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.ToTable("Pacientes");
                entity.Property(p => p.FechaNacimiento)
                    .IsRequired()
                    .HasColumnType("date");
                entity.Property(p => p.ObraSocial)
                    .IsRequired(false)
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}