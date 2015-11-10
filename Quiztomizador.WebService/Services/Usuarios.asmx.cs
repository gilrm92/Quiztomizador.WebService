using Quiztomizador.WebService.ContextConfiguration;
using Quiztomizador.WebService.Entidades;
using Quiztomizador.WebService.Model.IRepositorios;
using Quiztomizador.WebService.Model.Servicos;
using Quiztomizador.WebService.NinjectConfiguration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
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
        public bool Login(string email, string senha)
        {
            using (var context = new Context()) 
            {
                var usuario = context.Usuarios.Where(u => u.Email.Equals(email) && u.Senha.Equals(senha)).FirstOrDefault();
                if (usuario != null)
                {
                    //var gerenciadorDeSessao = new GerenciadorDeSessao(context);
                    //gerenciadorDeSessao.CriarSessao(usuario);
                    return true;
                }
                else 
                {
                    return false;
                }
            };
        }

        
    }
}
