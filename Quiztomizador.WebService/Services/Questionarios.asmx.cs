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
        public void Criar(string descricao, int idCategoria, int idUsuarioCadastrou)
        {
            Context.Response.Clear();
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
                    uid = questionario.IdQuestionario,
                    descricao = questionario.Descricao,
                    categoria = questionario.IdCategoria,
                    criador = questionario.IdUsuarioCriador
                    
                };

                var serializer = new JavaScriptSerializer();
                Context.Response.Write(serializer.Serialize(retornoAnon));
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
                if (questionario != null)
                {
                    questionario.Descricao = descricao;
                    questionario.Categoria = categoria;
                    questionario.IdCategoria = idCategoria;

                    context.Set<Questionario>().Attach(questionario);
                    context.Entry(questionario).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Questionário não existe.");
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Excluir(int idQuestionario)
        {
            using (var context = new Context())
            {
                var questionario = context.DbQuestionarios.Where(q => q.IdQuestionario.Equals(idQuestionario)).FirstOrDefault();

                if (questionario != null)
                {
                    questionario.Excluido = true;
                    context.Set<Questionario>().Attach(questionario);
                    context.Entry(questionario).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Questionário não existe.");
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Retornar(int idQuestionario)
        {

            Context.Response.Clear();
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
                Context.Response.Write(serializer.Serialize(anonObj));
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RetornarPorUsuario(int idUsuario)
        {
            Context.Response.Clear();
            using (var context = new Context())
            {
                var questionarios = context.DbQuestionarios.Where(q => q.UsuarioCriador.Equals(idUsuario) && !q.Excluido).ToList();
               
                foreach (var questionario in questionarios) 
                {
                    var categoria = context.DbCategorias.Where(c => c.IdCategoria.Equals(questionario.IdQuestionario)).FirstOrDefault();
                    var questoes = context.DbQuestoes.Where(q => q.IdQuestionario.Equals(questionario.IdQuestionario) && !q.Excluido).ToList();

                    questionario.Questoes = questoes;
                    questionario.Categoria = categoria;
                }

                var anonObj = questionarios.Select(questionario => new
                                            {
                                                uid = questionario.IdQuestionario,
                                                descricao = questionario.Descricao,
                                                categoria = new
                                                {
                                                    uid = questionario.Categoria.IdCategoria,
                                                    descricao = questionario.Categoria.Descricao
                                                },
                                                questoes = questionario.Questoes.Select(q => new
                                                                                    {
                                                                                        uid = q.IdQuestao,
                                                                                        titulo = q.Titulo,
                                                                                        tipoQuestao = q.TipoQuestao.ToString()
                                                                                    })
                                            });

                var serializer = new JavaScriptSerializer();
                Context.Response.Write(serializer.Serialize(anonObj));
       

            }
        }
    }
}
