using Quiztomizador.WebService.ContextConfiguration;
using Quiztomizador.WebService.Model.Entidades;
using Quiztomizador.WebService.Model.IRepositorios;
using Quiztomizador.WebService.Model.Servicos;
using Quiztomizador.WebService.NinjectConfiguration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Quiztomizador.WebService.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [ToolboxItem(false)]
    public class Usuarios : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Retornar(int idUsuario) 
        {
            Context.Response.Clear();
            using (var context = new Context())
            {
                var usuario = context.DbUsuarios.Where(u => u.IdUsuario.Equals(idUsuario)).FirstOrDefault();
                var serializer = new JavaScriptSerializer();

                if (usuario != null)
                {
                    var retornoAnon = new
                       {
                           uid = usuario.IdUsuario,
                           nome = usuario.Nome,
                           email = usuario.Email,

                       };
                    Context.Response.Write(serializer.Serialize(retornoAnon));
                }
                else
                    throw new Exception("Usuario não existe.");
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Logar(string email, string senha)
        {
            Context.Response.Clear();
            using (var context = new Context()) 
            {
                var usuario = context.DbUsuarios.Where(u => u.Email.Equals(email) && u.Senha.Equals(senha)).FirstOrDefault();
                var serializer = new JavaScriptSerializer();
                if (usuario != null)
                {
                    var retornoAnon = new 
                    {
                        uid = usuario.IdUsuario,
                        nome = usuario.Nome,
                        email = usuario.Email,

                    };
                    Context.Response.Write(serializer.Serialize(retornoAnon));
                }
                else
                    throw new Exception("Erro na autenticacao");
  
            };
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Criar(string nome, string email, string senha)
        {
            Context.Response.Clear();
            using (var context = new Context())
            {
                var usuario = context.DbUsuarios.Where(u => u.Email.Equals(email)).FirstOrDefault();
                if (usuario == null)
                {
                    var usuarioNew = new Usuario
                    {
                        Email = email,
                        Nome = nome,
                        Senha = senha
                    };
                    context.Set<Usuario>().Add(usuarioNew);
                    context.SaveChanges();
                    Context.Response.Write(usuarioNew.IdUsuario);
                }
                else
                {
                    throw new Exception("Email ja cadastrado!");
                }
            };

        }
    }
}
