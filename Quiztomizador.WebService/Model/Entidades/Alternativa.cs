using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quiztomizador.WebService.Model.Entidades
{
    public class Alternativa
    {
        public int IdAlternativa { get; set; }
        public string Titulo { get; set; }
        public bool AlternativaCorreta { get; set; }
        public int IdQuestao { get; set; }
        public Questao Questao { get; set; }
        public bool Excluido { get; set; }
    }
}
