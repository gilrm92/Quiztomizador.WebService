using Quiztomizador.WebService.Model.Entidades;
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
        public DbSet<Questionario> DbQuestionarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);
            modelBuilder.Entity<Usuario>().Property(u => u.IdUsuario).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            modelBuilder.Entity<Usuario>()
                        .HasMany<Categoria>(s => s.Categorias)
                        .WithRequired(s => s.Usuario)
                        .HasForeignKey(s => s.IdUsuario);

            modelBuilder.Entity<Categoria>().HasKey(c => c.IdCategoria);
                modelBuilder.Entity<Categoria>().Property(c => c.IdCategoria).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Questionario>().HasKey(q => q.IdQuestionario);
            modelBuilder.Entity<Questionario>().Property(q => q.IdQuestionario).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Questionario>().HasRequired(q => q.Categoria).WithMany().HasForeignKey(c => c.IdCategoria);
            modelBuilder.Entity<Questionario>().HasRequired(q => q.UsuarioCriador).WithMany().HasForeignKey(c => c.IdUsuarioCriador);
        }
    }
}