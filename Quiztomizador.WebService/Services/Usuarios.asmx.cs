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
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Usuarios : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object Retorna(string idUsuario) 
        {
            using (var context = new Context())
            {
                var idUsuarioConvertido = int.Parse(idUsuario);
                var usuario = context.DbUsuarios.Where(u => u.IdUsuario.Equals(idUsuarioConvertido)).FirstOrDefault();
                var serializer = new JavaScriptSerializer();

                if(usuario != null)
                    return serializer.Serialize(usuario);
                else
                    throw new Exception("Usuario não existe.");
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool Logar(string email, string senha)
        {
            using (var context = new Context()) 
            {
                var usuario = context.DbUsuarios.Where(u => u.Email.Equals(email) && u.Senha.Equals(senha)).FirstOrDefault();
                return usuario != null;
            };
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int Criar(string nome, string email, string senha)
        {
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

                    return usuarioNew.IdUsuario;
                }
                else 
                {
                    throw new Exception("Usuario já existe.");   
                }
            };
        }
    }
}
