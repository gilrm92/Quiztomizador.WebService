using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Quiztomizador.WebService.Model.Entidades
{
    [Table("Teste")]
    public class Teste
    {
        public int IdTeste { get; set; }
        public string Descricao { get; set; }        
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public int Erros {get; set; }
        public int Acertos { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuarios { get; set; }        
    }
}
