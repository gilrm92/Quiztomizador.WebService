using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quiztomizador.WebService.Model.Entidades
{
    public class Grupo
    {
        public int IdGrupo { get; set; }
        public string Nome { get; set; }
        public IList<Usuario> Participantes { get; set; }
        public IList<Questionario> QuestionariosCompartilhados { get; set; }
    }
}
