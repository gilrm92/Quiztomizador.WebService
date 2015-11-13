using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Quiztomizador.WebService.Entidades
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Descricao { get; set; }
        public bool Excluido { get; set; }
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
