using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiztomizador.WebService.Model.IRepositorios
{
    public interface IRepositorioBase<T> where T : class
    {
        T Salvar(T entity);
        IList<T> SalvarColecao(IList<T> collection);

        IList<T> RetornarTodos();
        T Retornar(int id);

        void Excluir(T entity);
        void ExcluirColecao(IList<T> collection);
    }
}
