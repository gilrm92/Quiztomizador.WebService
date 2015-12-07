using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quiztomizador.WebService.Model.Entidades;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quiztomizador.WebService.Model.Entidades
{
    [Table("Questionario")]
    public class Questionario
    {
        public Questionario()
        {
            Excluido = false;
            Publico = false;
            this.Usuarios = new HashSet<Usuario>(); 
        }

        public int IdQuestionario { get; set; }
        public string Descricao { get; set; }
        public bool Excluido { get; set; }
        public bool Publico { get; set; }
        public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; }
        public int IdUsuarioCriador { get; set; }
       // public Usuario UsuarioCriador { get; set; }
        public IList<Questao> Questoes { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
        
    }
}
