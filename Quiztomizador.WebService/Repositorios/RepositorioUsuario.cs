using Quiztomizador.WebService.Model.Entidades;
using Quiztomizador.WebService.Model.IRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiztomizador.WebService.Repositorios
{
    public class RepositorioUsuario : RepositorioBase<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario() : base()
        {
            
        }
    }
}