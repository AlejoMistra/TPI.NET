using Microsoft.EntityFrameworkCore;
using Domain.Model;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }

        public TPIContext(DbContextOptions<TPIContext> options) : base(options)
        {
            this.Database.EnsureCreated();
            //SeedInitialData();
        }

        internal TPIContext()
        {
            this.Database.EnsureCreated();
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

            modelBuilder.Entity<Profesional>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Nombre).IsRequired();
                entity.Property(p => p.Apellido).IsRequired();
                entity.Property(p => p.Matricula).IsRequired();
                //entity.HasMany(p => p.Especialidades)
                //      .WithMany(e => e.Profesionales)
                //      .UsingEntity(j => j.ToTable("ProfesionalEspecialidad"));
            });

        }

    }
}