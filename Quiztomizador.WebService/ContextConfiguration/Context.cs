using Quiztomizador.WebService.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Quiztomizador.WebService.ContextConfiguration
{
    public class Context : DbContext
    {
        public Context()
            : base("name=QuiztomizadorConnString")
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);
        }
    }
}