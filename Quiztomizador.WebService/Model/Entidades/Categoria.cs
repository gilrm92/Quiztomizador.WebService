using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Quiztomizador.WebService.Model.Entidades
{
    [Table("Categoria")]
    public class Categoria
    {

        public Categoria()
        {
            this.Usuarios = new HashSet<Usuario>();
        }

        public int IdCategoria { get; set; }
        public string Descricao { get; set; }
        public bool Excluido { get; set; }
        public int IdUsuarioCriador { get; set; }
      //  public Usuario UsuarioCriador { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }

      

    }
}
