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
        public object Criar(string descricao, string idUsuarioCriador)
        {
            using (var context = new Context())
            {
                var idUsuarioConvertido = int.Parse(idUsuarioCriador);
                var categoria = context.DbCategorias.Where(c => c.Descricao.Equals(descricao) && c.IdUsuario == idUsuarioConvertido).FirstOrDefault();
                
                if (categoria == null)
                {
                    var categoriaNew = new Categoria
                    {
                        Descricao = descricao,
                        Usuario = context.DbUsuarios.Where(u => u.IdUsuario == idUsuarioConvertido).FirstOrDefault(),
                        IdUsuario = idUsuarioConvertido
                    };

                    context.Set<Categoria>().Add(categoriaNew);
                    context.SaveChanges();

                    var anonObj = new 
                    {
                        uId = categoriaNew.IdCategoria,
                        descricao = categoriaNew.Descricao,
                        idUsuarioCriador = categoriaNew.IdUsuario
                    };

                    var serializer = new JavaScriptSerializer();
                    return serializer.Serialize(anonObj);
                }
                else
                {
                    throw new Exception("Já existe uma categoria com esse nome para esse usuário.");
                }
            };
        }
        
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object Alterar(string idCategoria, string descricao)
        {
            using (var context = new Context())
            {
                var idCategoriaConvertido = int.Parse(idCategoria);
                var categoria = context.DbCategorias.Where(c => c.IdCategoria.Equals(idCategoriaConvertido)).FirstOrDefault();
                if (categoria != null)
                {
                    categoria.Descricao = descricao;
                    context.Set<Categoria>().Attach(categoria);
                    context.Entry(categoria).State = EntityState.Modified;
                    context.SaveChanges();

                    var anonObj = new
                    {
                        uId = categoria.IdCategoria,
                        descricao = categoria.Descricao
                    };

                    var serializer = new JavaScriptSerializer();
                    return serializer.Serialize(anonObj);
                }
                else
                {
                    throw new Exception("Categoria não existe.");
                }
            };
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Excluir(string idCategoria)
        {
            using (var context = new Context())
            {
                var idCategoriaConvertido = int.Parse(idCategoria);
                var categoria = context.DbCategorias.Where(c => c.IdCategoria.Equals(idCategoriaConvertido)).FirstOrDefault();
                
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
        public object Retorna(string idUsuario)
        {
            using (var context = new Context())
            {
                var idUsuarioConvertido = int.Parse(idUsuario);
                var categoria = context.DbCategorias.Where(c => c.IdUsuario.Equals(idUsuarioConvertido) && !c.Excluido).ToList();
               
                var anonObj = categoria.Select(c => new
                    {
                        uId = c.IdCategoria,
                        descricao = c.Descricao,
                        uIdUsuario = c.IdUsuario
                    });

                var serializer = new JavaScriptSerializer();
                return serializer.Serialize(anonObj);
            };
        }
    }
}
