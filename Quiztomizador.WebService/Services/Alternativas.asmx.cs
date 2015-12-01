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
    /// Summary description for Alternativas
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class Alternativas : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Criar(int idQuestao, string titulo, bool correta)
        {
            using (var context = new Context()) 
            {
                var questao = context.DbQuestoes.Where(q => q.IdQuestao.Equals(idQuestao)).FirstOrDefault();

                var alternativa = new Alternativa
                {
                    Titulo = titulo,
                    IdQuestao = idQuestao,
                    Questao = questao,
                    AlternativaCorreta = correta
                };

                context.Set<Alternativa>().Add(alternativa);
                context.SaveChanges();

                var anonObj = new
                {
                    uid = alternativa.IdAlternativa,
                    titulo = alternativa.Titulo,
                    alternativaCorreta = alternativa.AlternativaCorreta
                };

                var serializer = new JavaScriptSerializer();
                return serializer.Serialize(anonObj);
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Editar(int idAlternativa, string newTitulo, bool correta)
        {
            using (var context = new Context())
            {
                var alternativa = context.DbAlternativas.Where(q => q.IdAlternativa.Equals(idAlternativa)).FirstOrDefault();
                
                alternativa.Titulo = newTitulo;
                alternativa.AlternativaCorreta = correta;

                context.Set<Alternativa>().Attach(alternativa);
                context.Entry(alternativa).State = EntityState.Modified;
                context.SaveChanges();

            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Excluir(int idAlternativa)
        {
            using (var context = new Context())
            {
                var alternativa = context.DbAlternativas.Where(q => q.IdAlternativa.Equals(idAlternativa)).FirstOrDefault();
                alternativa.Excluido = true;

                context.Set<Alternativa>().Attach(alternativa);
                context.Entry(alternativa).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Retornar(int idQuestao)
        {
            using (var context = new Context())
            {
                var alternativas = context.DbAlternativas.Where(q => q.IdQuestao.Equals(idQuestao) && !q.Excluido).ToList();
                var anonObj = alternativas.Select(alternativa => new
                {
                    uid = alternativa.IdAlternativa,
                    titulo = alternativa.Titulo,
                    alternativaCorreta = alternativa.AlternativaCorreta
                });

                var serializer = new JavaScriptSerializer();
                return serializer.Serialize(anonObj);
            }
        }
    }
}
