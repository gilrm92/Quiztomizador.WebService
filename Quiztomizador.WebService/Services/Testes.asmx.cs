using Quiztomizador.WebService.ContextConfiguration;
using Quiztomizador.WebService.Model.Entidades;
using Quiztomizador.WebService.Model.Servicos;
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
    public class Testes : System.Web.Services.WebService
    { 

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Postar(int idUsuario, string descricao, String inicio, string termino, int acertos, int erros)
        {
            Context.Response.Clear();
            using (var context = new Context())
            {
                var usuario = context.DbUsuarios.Where(u => u.IdUsuario.Equals(idUsuario)).FirstOrDefault();
                if (usuario != null)
                {
                    var testeNew = new Teste
                    {
                        IdUsuario = idUsuario,
                        Descricao = descricao,
                        Inicio = Convert.ToDateTime(inicio),
                        Termino = Convert.ToDateTime(termino),
                        Acertos = acertos,
                        Erros = erros,
                    };
                    context.Set<Teste>().Add(testeNew);
                    context.SaveChanges();

                    Context.Response.Write(testeNew.IdTeste);
                }
                else 
                {
                    throw new Exception("Usuario não encontrado na base de dados!");   
                }
            };
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Listar(int idUsuario)
        {
            Context.Response.Clear();
            using (var context = new Context())
            {
                var teste = context.DbTestes.Where(c => c.IdUsuario.Equals(idUsuario)).ToList();
                var serializer = new JavaScriptSerializer();

                var anonObj = teste.Select(t => new
                {
                    uId = t.IdTeste,
                    descricao = t.Descricao,
                    data_inicio = t.Inicio.ToString("dd-MM-yyyy HH:mm:ss"),
                    data_termino = t.Termino.ToString("dd-MM-yyyy HH:mm:ss"),
                    erros = t.Erros,
                    acertos = t.Acertos
                });
                Context.Response.Write(serializer.Serialize(anonObj));
            };
        }


    }
}
