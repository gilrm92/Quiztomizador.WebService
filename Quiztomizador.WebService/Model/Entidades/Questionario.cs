using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quiztomizador.WebService.Model.Entidades;

namespace Quiztomizador.WebService.Entidades
{
    public class Questionario
    {
        public int IdQuestionario { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IList<Questao> Questoes { get; set; }

    }
}
