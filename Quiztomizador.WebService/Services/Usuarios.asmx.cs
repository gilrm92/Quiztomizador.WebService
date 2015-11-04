using Quiztomizador.WebService.Entidades;
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
        public string CriarUsuario()
        {
            return "Usuario:";
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string Logar()
        {
            return "Usuario:";
        }

        
    }
}
