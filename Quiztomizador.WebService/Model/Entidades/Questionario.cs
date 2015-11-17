using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quiztomizador.WebService.Model.Entidades;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quiztomizador.WebService.Model.Entidades
{
    public class Questionario
    {
        public Questionario()
        {
            Excluido = false;
            DataCriacao = DateTime.Now;
        }

        public int IdQuestionario { get; set; }
        public string Descricao { get; set; }
        public bool Excluido { get; set; }
        public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; }
        public int IdUsuarioCriador { get; set; }
        public Usuario UsuarioCriador { get; set; }
        public DateTime DataCriacao { get; set; }
        //public IList<Questao> Questoes { get; set; }
    }
}
