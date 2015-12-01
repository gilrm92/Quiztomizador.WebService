using Quiztomizador.WebService.ContextConfiguration;
using Quiztomizador.WebService.Model.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
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
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
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

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Editar(int idQuestionario, string descricao, int idCategoria)
        {
            using (var context = new Context())
            {
                var questionario = context.DbQuestionarios.Where(q => q.IdQuestionario.Equals(idQuestionario)).FirstOrDefault();
                var categoria = context.DbCategorias.Where(c => c.IdCategoria.Equals(idCategoria)).FirstOrDefault();

                questionario.Descricao = descricao;
                questionario.Categoria = categoria;
                questionario.IdCategoria = idCategoria;

                context.Set<Questionario>().Attach(questionario);
                context.Entry(questionario).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Excluir(int idQuestionario)
        {
            using (var context = new Context())
            {
                var questionario = context.DbQuestionarios.Where(q => q.IdQuestionario.Equals(idQuestionario)).FirstOrDefault();

                questionario.Excluido = true;

                context.Set<Questionario>().Attach(questionario);
                context.Entry(questionario).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object Retornar(int idQuestionario)
        {
            using (var context = new Context())
            {
                var questionario = context.DbQuestionarios.Where(q => q.IdQuestionario.Equals(idQuestionario) && !q.Excluido).FirstOrDefault();
                var categoria = context.DbCategorias.Where(c => c.IdCategoria.Equals(questionario.IdQuestionario)).FirstOrDefault();
                var questoes = context.DbQuestoes.Where(q => q.IdQuestionario.Equals(questionario.IdQuestionario) && !q.Excluido).ToList();
                
                questionario.Questoes = questoes;
                questionario.Categoria = categoria;

                var anonObj = new
                {
                    uid = questionario.IdQuestionario,
                    descricao = questionario.Descricao,
                    categoria = new { uid = questionario.Categoria.IdCategoria, descricao = questionario.Categoria.Descricao },
                    questoes = questionario.Questoes.Select(q => new { uid = q.IdQuestao, titulo = q.Titulo, tipoQuestao = q.TipoQuestao.ToString() })
                };

                var serializer = new JavaScriptSerializer();
                return serializer.Serialize(anonObj);
            }
        }

    }
}
