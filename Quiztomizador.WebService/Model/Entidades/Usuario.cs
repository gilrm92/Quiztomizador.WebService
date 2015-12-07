using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Quiztomizador.WebService.Model.Entidades
{
    [Table("Usuario")]
    public class Usuario
    {
        public Usuario()
        {
            this.Categorias = new HashSet<Categoria>();
            this.Questionarios = new HashSet<Questionario>();
        }
        
        public int IdUsuario { get; set;}
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public ICollection<Categoria> Categorias { get; set; }
        public ICollection<Questionario> Questionarios { get; set; }
      
    }
}