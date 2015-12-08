using Quiztomizador.WebService.ContextConfiguration;
using Quiztomizador.WebService.Model.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Quiztomizador.WebService.Services
{
    /// <summary>
    /// Summary description for Categoria
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class Categorias : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Criar(string descricao, int idUsuarioCriador)
        {
            using (var context = new Context())
            {
                Context.Response.Clear();
                var usuario = context.DbUsuarios.Where(u => u.IdUsuario == idUsuarioCriador).FirstOrDefault();
                // caso não encontre o usuário no banco não inclui a categoria
                if (usuario == null)
                {
                    throw new Exception("Usuario nao cadastrado!");
                }
                var categoria = context.DbCategorias.Where(c => c.Descricao.Equals(descricao) && c.IdUsuarioCriador == idUsuarioCriador && !c.Excluido).FirstOrDefault();
               
                if (categoria == null)
                {
                    var categoriaNew = new Categoria
                    {
                        Descricao = descricao,
                        IdUsuarioCriador = idUsuarioCriador,
                        UsuarioCriador  = usuario                      
                    };
                    context.Set<Categoria>().Add(categoriaNew);
                    context.SaveChanges();

                    var anonObj = new 
                    {
                        uid = categoriaNew.IdCategoria,
                        descricao = categoriaNew.Descricao,
                        criador = categoriaNew.IdUsuarioCriador
                    };

                    var serializer = new JavaScriptSerializer();
                    Context.Response.Write(serializer.Serialize(anonObj));

                }
                else
                {
                    throw new Exception("Já existe uma categoria com esse nome para esse usuário.");
                }
            };
        }
        
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Alterar(int idCategoria, string descricao)
        {
            using (var context = new Context())
            {               
                var categoria = context.DbCategorias.Where(c => c.IdCategoria.Equals(idCategoria)).FirstOrDefault();
                if (categoria != null)
                {
                    categoria.Descricao = descricao;
                    context.Set<Categoria>().Attach(categoria);
                    context.Entry(categoria).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Categoria não existe.");
                }
            };
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Excluir(int idCategoria)
        {
            using (var context = new Context())
            {
                var categoria = context.DbCategorias.Where(c => c.IdCategoria.Equals(idCategoria)).FirstOrDefault();
                
                if (categoria != null)
                {
                    categoria.Excluido = true;
                    context.Set<Categoria>().Attach(categoria);
                    context.Entry(categoria).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Categoria não existe.");
                }
            };
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Retornar(int idCategoria)
        {
            Context.Response.Clear();
            using (var context = new Context())
            {
                var categoria = context.DbCategorias.Where(c => c.IdCategoria.Equals(idCategoria)).ToList();

                var anonObj = categoria.Select(c => new
                {
                    uid = c.IdCategoria,
                    descricao = c.Descricao,
                    criador = c.IdUsuarioCriador
                });

                var serializer = new JavaScriptSerializer();
                Context.Response.Write(serializer.Serialize(anonObj));
            };
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RetornarPorUsuario(int idUsuario)
        {
            Context.Response.Clear();
            using (var context = new Context())
            {
                var categoria = context.DbCategorias.Where(c => c.IdUsuarioCriador.Equals(idUsuario) && !c.Excluido).ToList();
               
                var anonObj = categoria.Select(c => new
                    {
                        uid = c.IdCategoria,
                        descricao = c.Descricao,
                        criador = c.IdUsuarioCriador
                    });

                var serializer = new JavaScriptSerializer();
                Context.Response.Write(serializer.Serialize(anonObj));
            };
        }

    }
}
