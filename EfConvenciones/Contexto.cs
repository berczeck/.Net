using System;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EfConvenciones
{
    public class Contexto : DbContext
    {
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Organizacion> Organizacion { get; set; }

        public Contexto()
        {
            Database.SetInitializer<Contexto>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions
                .Remove<NavigationPropertyNameForeignKeyDiscoveryConvention>();
            modelBuilder.Conventions.Remove<StoreGeneratedIdentityKeyConvention>();

            modelBuilder.Properties<DateTime>()
                .Configure(c => c.HasColumnType("datetime"));
            modelBuilder.Properties<string>()
                .Configure(c => c.HasColumnType("varchar"));
            modelBuilder.Properties<string>()
                .Configure(c => c.HasMaxLength(500));
            modelBuilder.Properties()
                .Where(x => x.Name == "Id")
                .Configure(c => c.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity));
            modelBuilder.Properties<DateTime>()
                .Where(x => x.Name == "FechaRegistro")
                .Configure(c => c.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed));
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Ignore<ICollection>();

            modelBuilder.Entity<Organizacion>().HasKey(x => new {x.EmpresaId, x.PaisId});
        }

    }
}
