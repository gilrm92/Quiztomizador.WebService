using Quiztomizador.WebService.ContextConfiguration;
using Quiztomizador.WebService.Model.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace Quiztomizador.WebService.Services
{
    /// <summary>
    /// Summary description for Questionarios
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Questionarios : System.Web.Services.WebService
    {

        [WebMethod]
        public string Criar(string descricao, int idCategoria, int idUsuarioCadastrou)
        {
            using (var context = new Context())
            {
                var categoria = context.DbCategorias.Where(c => c.IdCategoria.Equals(idCategoria)).FirstOrDefault();
                var usuario = context.DbUsuarios.Where(c => c.IdUsuario.Equals(idUsuarioCadastrou)).FirstOrDefault();

                var questionario = new Questionario
                {
                    Descricao = descricao,
                    IdCategoria = idCategoria,
                    Categoria = categoria,
                    IdUsuarioCriador = idUsuarioCadastrou,
                    UsuarioCriador = usuario
                };

                context.Set<Questionario>().Add(questionario);
                context.SaveChanges();

                var retornoAnon = new
                {
                    uId = questionario.IdQuestionario,
                    descricao = questionario.Descricao,
                    idUsuarioCadastrou = questionario.IdUsuarioCriador,
                    dataCadastro = questionario.DataCriacao,
                    idCategoria = questionario.IdCategoria
                };

                var serializer = new JavaScriptSerializer();
                return serializer.Serialize(retornoAnon);
            }
            
        }
    }
}
