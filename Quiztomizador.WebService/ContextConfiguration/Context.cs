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
        public DbSet<Questao> DbQuestoes { get; set; }
        public DbSet<Alternativa> DbAlternativas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);
            modelBuilder.Entity<Usuario>().Property(u => u.IdUsuario).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Usuario>()
                  .HasMany(c => c.Categorias)
                  .WithMany(u => u.Usuarios)
                  .Map(x =>
                  {
                      x.MapRightKey("IdCategoria");
                      x.MapLeftKey("IdUsuario");
                      x.ToTable("Categoria_Usuario");                      
                  });
            modelBuilder.Entity<Usuario>()
                 .HasMany(q => q.Questionarios)
                 .WithMany(u => u.Usuarios)
                 .Map(x =>
                 {
                     x.MapRightKey("IdQuestionario");
                     x.MapLeftKey("IdUsuario");
                     x.ToTable("Questionario_Usuario");
                 });

           // modelBuilder.Entity<Usuario>().HasMany<Categoria>(s => s.Categorias).WithRequired(s => s.UsuarioCriador).HasForeignKey(s => s.IdUsuarioCriador);
           // modelBuilder.Entity<Usuario>().HasMany<Questionario>(s => s.Questionarios).WithRequired(s => s.UsuarioCriador).HasForeignKey(s => s.IdUsuarioCriador);


            modelBuilder.Entity<Categoria>().HasKey(c => c.IdCategoria);
            modelBuilder.Entity<Categoria>().Property(c => c.IdCategoria).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Questionario>().HasKey(q => q.IdQuestionario);
            modelBuilder.Entity<Questionario>().Property(q => q.IdQuestionario).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Questionario>().HasRequired(q => q.Categoria).WithMany().HasForeignKey(c => c.IdCategoria);
          //  modelBuilder.Entity<Questionario>().HasRequired(q => q.UsuarioCriador).WithMany().HasForeignKey(c => c.IdUsuarioCriador);
            modelBuilder.Entity<Questionario>().HasMany(q => q.Questoes).WithRequired(q => q.Questionario).HasForeignKey(q => q.IdQuestionario);
                        
            modelBuilder.Entity<Questao>().HasKey(q => q.IdQuestao);
            modelBuilder.Entity<Questao>().Property(q => q.IdQuestao).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Questao>().HasMany(q => q.Alternativas).WithRequired(q => q.Questao).HasForeignKey(q => q.IdQuestao);

            modelBuilder.Entity<Alternativa>().HasKey(q => q.IdAlternativa);
            modelBuilder.Entity<Alternativa>().Property(q => q.IdAlternativa).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            base.OnModelCreating(modelBuilder);
        }
    }
}