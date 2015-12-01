using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quiztomizador.WebService.Model.Entidades
{
    public class Questao
    {
        public int IdQuestao { get; set; }
        public string Titulo { get; set; }
        public int IdQuestionario { get; set; }
        public Questionario Questionario { get; set; }
        public TipoQuestao TipoQuestao { get; set; }
        public IList<Alternativa> Alternativas { get; set; }
        
    }
}
