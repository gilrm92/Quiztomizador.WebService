using Quiztomizador.WebService.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public DbSet<Usuario> DbUsuarios { get; set; }
        public DbSet<Categoria> DbCategorias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            modelBuilder.Entity<Usuario>().Property(u => u.IdUsuario).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Usuario>().HasKey(u => new { u.IdUsuario });
            modelBuilder.Entity<Usuario>()
                        .HasMany<Categoria>(s => s.Categorias)
                        .WithRequired(s => s.Usuario)
                        .HasForeignKey(s => s.IdUsuario);

            modelBuilder.Entity<Categoria>().HasKey(c => c.IdCategoria);
            modelBuilder.Entity<Categoria>().Property(c => c.IdCategoria).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}