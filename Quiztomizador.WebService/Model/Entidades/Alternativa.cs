using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Quiztomizador.WebService.Model.Entidades
{
    [Table("Alternativa")]
    public class Alternativa
    {
        public int IdAlternativa { get; set; }
        public string Descricao { get; set; }
        public bool Certa { get; set; }
        public int IdQuestao { get; set; }
        public Questao Questao { get; set; }
        public bool Excluido { get; set; }
    }
}
