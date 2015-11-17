using Quiztomizador.WebService.ContextConfiguration;
using Quiztomizador.WebService.Model.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiztomizador.WebService.Model.Servicos
{
    public class GerenciadorDeSessao
    {
        private Context _context;

        public GerenciadorDeSessao(ContextConfiguration.Context context)
        {
            _context = context;
        }
        public string CriarSessao(Usuario usuario)
        {
            var sqlString = string.Format(@"INSERT INTO Usuarios_Sessao VALUES({0},{1},'{2}' )", usuario.IdUsuario, DateTime.Now, "M@uro");
            _context.Database.ExecuteSqlCommand(sqlString, null);
            var sessao = GetSessaoDeUsuario(usuario);
            return sessao;
        }

        private string GetSessaoDeUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}