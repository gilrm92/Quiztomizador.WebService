using Quiztomizador.WebService.ContextConfiguration;
using Quiztomizador.WebService.Model.IRepositorios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Quiztomizador.WebService.Repositorios
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T: class
    {
        protected Context Context;

        protected RepositorioBase()
        {
            Context = GerenciadorDeContexto.GetInstance().GetContext();
        } 

        public T Salvar(T entity)
        {
            if (Int32.Parse((entity).GetType().GetProperty("Id").GetValue(entity).ToString()) > 0)
            {
                Context.Set<T>().Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                Context.Set<T>().Add(entity);
            }

            Context.SaveChanges();
            return entity;
        }

        public IList<T> SalvarColecao(IList<T> collection)
        {
            foreach (var entity in collection)
            {
                if (Int32.Parse((entity).GetType().GetProperty("Id").GetValue(entity).ToString()) > 0)
                {
                    Context.Set<T>().Attach(entity);
                    Context.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    Context.Set<T>().Add(entity);
                }
            }

            Context.SaveChanges();
            return collection;
        }

        public IList<T> RetornarTodos()
        {
            return Context.Set<T>().ToList();
        }

        public T Retornar(int id)
        {
            var entity = Context.Set<T>().Find(id);
            return entity;
        }

        public void Excluir(T entity)
        {
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
        }

        public void ExcluirColecao(IList<T> collection)
        {
            foreach (var item in collection)
                Context.Set<T>().Remove(item);
            
            Context.SaveChanges();
        }
    }
}