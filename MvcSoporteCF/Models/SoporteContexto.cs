using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MvcSoporteCF.Models
{
    public class SoporteContexto : DbContext
    {
        public SoporteContexto() : base("name=SoporteContexto")
        {
        }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<TipoAveria> TipoAverias { get; set; }
        public DbSet<Localizacion> Localizaciones { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Aviso> Avisos { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}