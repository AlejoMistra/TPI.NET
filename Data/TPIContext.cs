using Microsoft.EntityFrameworkCore;
using Domain.Model;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Usuario> Usuarios {  get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<HistoriaClinica> HistoriasClinicas { get; set; }
        public DbSet<RegistroClinico> RegistrosClinicos { get; set; }

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
            modelBuilder.Ignore<Factura>();

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(44);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(44);

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasConversion<string>()
                    .HasMaxLength(30);

                entity.Property(e => e.FechaCreacion)
                    .IsRequired();

                entity.Property(e => e.Activo)
                    .IsRequired();

                entity.HasOne(e => e.Persona)
                    .WithMany()
                    .HasForeignKey(e => e.PersonaId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(e => e.PersonaId)
                .IsUnique()
                .HasFilter("[PersonaId] IS NOT NULL");
                // Restricciones únicas
                entity.HasIndex(e => e.Username)
                    .IsUnique();

                entity.HasIndex(e => e.Email)
                    .IsUnique();

            });

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
                    .IsRequired(false)
                    .HasMaxLength(100);
                entity.Property(p => p.Telefono)
                    .IsRequired(false)
                    .HasMaxLength(20);
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

            modelBuilder.Entity<Profesional>(entity =>
            {
                entity.ToTable("Profesionales");
                entity.Property(p => p.Matricula)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(p => p.Estado)
                    .IsRequired()
                    .HasConversion<string>()
                    .HasMaxLength(20);
                entity.HasOne(p => p.Especialidad)
                    .WithMany()
                    .HasForeignKey(p => p.EspecialidadId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<HistoriaClinica>(entity =>
            {
                entity.HasKey(h => h.Id);
                entity.Property(h => h.GrupoSanguineo)
                    .IsRequired()
                    .HasConversion<string>()
                    .HasMaxLength(20);
                entity.Property(h => h.FechaCreacion).IsRequired();

                entity.HasOne<Paciente>()
                    .WithOne(p => p.HistoriaClinica)
                    .HasForeignKey<HistoriaClinica>(h => h.PacienteId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(h => h.RegistrosClinicos)
                    .WithOne()
                    .HasForeignKey(r => r.HistoriaClinicaId)
                    .OnDelete(DeleteBehavior.Cascade);

                // EF debe usar el campo backing _registrosClinicos para la colección readonly
                entity.Navigation(h => h.RegistrosClinicos)
                    .UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            modelBuilder.Entity<RegistroClinico>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Tipo)
                    .IsRequired()
                    .HasConversion<string>()
                    .HasMaxLength(30);
                entity.Property(r => r.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500);
                entity.Property(r => r.Fecha).IsRequired();

                entity.HasOne<Profesional>()
                    .WithMany()
                    .HasForeignKey(r => r.ProfesionalId)
                    .OnDelete(DeleteBehavior.Restrict);

                // TurnoId: sin FK real mientras Turno esté en Ignore()
                // Cuando se implemente Turno, agregar la FK a Turno.Id
                entity.Property(r => r.TurnoId).IsRequired(false);
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
