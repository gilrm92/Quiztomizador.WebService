using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiztomizador.WebService.Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; set;}
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        //public IList<Categoria> Categorias { get; set; }
        //public IList<Questionario> Questionarios { get; set; }
        //public IList<Grupo> Grupos { get; set; }
        
    }
}