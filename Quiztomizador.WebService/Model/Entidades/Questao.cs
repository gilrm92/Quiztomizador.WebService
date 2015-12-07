using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Quiztomizador.WebService.Model.Entidades
{
    [Table("Questao")]
    public class Questao
    {
        public int IdQuestao { get; set; }
        public string Titulo { get; set; }
        public int IdQuestionario { get; set; }
        public Questionario Questionario { get; set; }
        public TipoQuestao Tipo { get; set; }
        public IList<Alternativa> Alternativas { get; set; }
        public bool Excluido { get; set; }

    }
}
