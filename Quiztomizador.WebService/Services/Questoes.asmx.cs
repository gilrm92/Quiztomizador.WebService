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
    /// Summary description for Questoes
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class Questoes : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object Criar(string titulo, int tipoQuestao, int idQuestionario)
        {
            using (var context = new Context()) 
            {
                var questionario = context.DbQuestionarios.Where(q => q.IdQuestionario.Equals(idQuestionario)).FirstOrDefault();
                var tipoQuestaoEnum = (TipoQuestao)tipoQuestao;

                var questao = new Questao
                {
                    Titulo = titulo,
                    Tipo = tipoQuestaoEnum,
                    Questionario = questionario,
                    IdQuestionario = idQuestionario
                };

                context.Set<Questao>().Add(questao);
                context.SaveChanges();

                var anonObj = new
                {
                    uid = questao.IdQuestao,
                    titulo =  questao.Titulo,
                    tipo = questao.Tipo.ToString()
                };

                var serializer = new JavaScriptSerializer();
                return serializer.Serialize(anonObj);
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Editar(int idQuestao, string titulo, int tipoQuestao)
        {
            using (var context = new Context())
            {
                var questao = context.DbQuestoes.Where(q => q.IdQuestao.Equals(idQuestao)).FirstOrDefault();
                var tipoQuestaoEnum = (TipoQuestao)tipoQuestao;

                questao.Tipo = tipoQuestaoEnum;
                questao.Titulo = titulo;

                context.Set<Questao>().Attach(questao);
                context.Entry(questao).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Excluir(int idQuestao)
        {
            using (var context = new Context())
            {
                var questao = context.DbQuestoes.Where(q => q.IdQuestao.Equals(idQuestao)).FirstOrDefault();
                questao.Excluido = true;
                
                context.Set<Questao>().Attach(questao);
                context.Entry(questao).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object Retornar(int idQuestao)
        {
            using (var context = new Context())
            {
                var questao = context.DbQuestoes.Where(q => q.IdQuestao.Equals(idQuestao) && !q.Excluido).FirstOrDefault();
                var alternativas = context.DbAlternativas.Where(a => a.IdQuestao.Equals(questao.IdQuestao)).ToList();

                questao.Alternativas = alternativas;
                var anonObj = new
                {
                    uid = questao.IdQuestao,
                    titulo = questao.Titulo,
                    tipo = questao.Tipo,
                    alternativas = alternativas.Select(a => new { uid = a.IdAlternativa, titulo = a.Descricao, correta = a.Certa })
                };

                var serializer = new JavaScriptSerializer();
                return serializer.Serialize(anonObj);
            }
        }

    }
}
