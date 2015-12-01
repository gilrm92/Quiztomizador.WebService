using Quiztomizador.WebService.ContextConfiguration;
using Quiztomizador.WebService.Model.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
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
                    TipoQuestao = tipoQuestaoEnum,
                    Questionario = questionario,
                    IdQuestionario = idQuestionario
                };
            }
            return "";
        }
    }
}
