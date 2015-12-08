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
        public void Criar(string descricao, int idCategoria, int idUsuarioCriador)
        {
            Context.Response.Clear();
            using (var context = new Context())
            {
                var categoria = context.DbCategorias.Where(c => c.IdCategoria.Equals(idCategoria)).FirstOrDefault();
                var usuario = context.DbUsuarios.Where(c => c.IdUsuario == idUsuarioCriador).FirstOrDefault();               

                var questionario  = new Questionario
                {
                    Descricao = descricao,
                    IdCategoria = idCategoria,
                    Categoria = categoria,
                    IdUsuarioCriador = idUsuarioCriador,
                   // UsuarioCriador = usuario
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
        public void Excluir(int idQuestionario, int idUsuario)
        {
            using (var context = new Context())
            {
                var questionario = context.DbQuestionarios.Where(q => q.IdQuestionario.Equals(idQuestionario)).FirstOrDefault();               
                var usuario = context.DbUsuarios.Where(u => u.IdUsuario.Equals(idUsuario)).First();

                if (questionario != null)
                {
                    // se o Usuário é o criador do questionario então remove da base
                    if (questionario.IdUsuarioCriador == idUsuario)
                    {
                        questionario.Excluido = true;
                        context.Set<Questionario>().Attach(questionario);
                        context.Entry(questionario).State = EntityState.Modified;                    
                    }
                    // senão remove da tabela de relacionamento 
                    else
                    {
                        questionario.Usuarios.Remove(usuario);       
                    }                                       
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
                var categoria = context.DbCategorias.Where(c => c.IdCategoria.Equals(questionario.IdCategoria)).FirstOrDefault();
               // var questoes = context.DbQuestoes.Where(q => q.IdQuestionario.Equals(questionario.IdQuestionario) && !q.Excluido).ToList();
                
               // questionario.Questoes = questoes;
                questionario.Categoria = categoria;
        

                var anonObj = new
                {
                    uid = questionario.IdQuestionario,
                    descricao = questionario.Descricao,
                   /////// criador = new { uid = questionario.IdUsuarioCriador, nome = questionario.UsuarioCriador.Nome, email = questionario.UsuarioCriador.Email },
                    categoria = new
                    {
                        uid = questionario.Categoria.IdCategoria,
                        descricao = questionario.Categoria.Descricao,
                        criador = new { uid = questionario.Categoria.IdUsuarioCriador, nome = questionario.Categoria.UsuarioCriador.Nome, email = questionario.Categoria.UsuarioCriador.Email }
                    }
                    //questoes = questionario.Questoes.Select(q => new { uid = q.IdQuestao, titulo = q.Titulo, tipo = q.Tipo.ToString() })
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

                var questionarios = context.DbQuestionarios.Where(q => q.IdUsuarioCriador.Equals(idUsuario) && !q.Excluido).ToList();
               
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
                                                                                        tipo = q.Tipo.ToString()
                                                                                    })
                                            });

                var serializer = new JavaScriptSerializer();
                Context.Response.Write(serializer.Serialize(anonObj));      
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Baixar(int idQuestionario, int idUsuario)
        {
            Context.Response.Clear();
            using (var context = new Context())
            {                
                
                var usuario = context.DbUsuarios.Where(c => c.IdUsuario == idUsuario).FirstOrDefault();
                var questionario = context.DbQuestionarios.Where(q => q.IdQuestionario.Equals(idQuestionario)
                    && !q.IdUsuarioCriador.Equals(idUsuario) && q.Publico == true).FirstOrDefault();
               
                if (questionario != null && usuario != null)
                {
                    // adiciona nas tabelas associativas questionario_usuario 
                    questionario.Usuarios.Add(usuario);
                    context.Set<Questionario>().Attach(questionario);
                    context.Entry(questionario).State = EntityState.Modified;
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
                else
                {
                    throw new Exception("Não foi possivel baixar o questionario");
                }
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Compartilhar(int idQuestionario, bool publico)
        {
            
            using (var context = new Context())
            {
                var questionario = context.DbQuestionarios.Where(q => q.IdQuestionario.Equals(idQuestionario)).FirstOrDefault();
                if (questionario != null)
                {
                    questionario.Publico = publico;

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
        public void RetornarCompartilhados()
        {
            Context.Response.Clear();
            using (var context = new Context())
            {
                var questionarios = context.DbQuestionarios.Where(q => q.Publico == true).ToList();
               
               var anonObj = questionarios.Select(q => new 
                {
                    uid = q.IdQuestionario,
                    descricao = q.Descricao,
                    criador = q.IdUsuarioCriador,
                    categoria = new
                    {
                        uid = q.Categoria.IdCategoria,
                        descricao = q.Categoria.Descricao,
                        criador = q.Categoria.IdUsuarioCriador
                        //criador = new { uid = questionario.Categoria.IdUsuarioCriador, nome = questionario.Categoria.UsuarioCriador.Nome, email = questionario.Categoria.UsuarioCriador.Email }
                    }
                });
                
                var serializer = new JavaScriptSerializer();
                Context.Response.Write(serializer.Serialize(anonObj));              
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RetornarBaixados(int idUsuario)
        {
            Context.Response.Clear();
            using (var context = new Context())
            {
                var usuario = context.DbUsuarios.Find(idUsuario);
                var questionarios = usuario.Questionarios;
               
                var anonObj = questionarios.Select(q => new
                {
                    uid = q.IdQuestionario,
                    descricao = q.Descricao,
                    criador = q.IdUsuarioCriador,
                    categoria = new
                    {
                        uid = q.Categoria.IdCategoria,
                        descricao = q.Categoria.Descricao,
                        criador = q.Categoria.IdUsuarioCriador
                        //criador = new { uid = questionario.Categoria.IdUsuarioCriador, nome = questionario.Categoria.UsuarioCriador.Nome, email = questionario.Categoria.UsuarioCriador.Email }
                    }
                });

                var serializer = new JavaScriptSerializer();
                Context.Response.Write(serializer.Serialize(anonObj));
            }
        }

    }
}
