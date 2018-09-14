using MacBlog.DAL.Contexto;
using MacBlog.DAL.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace MacBlog.DAL.Repositorio
{
    public class CategoriaRepositorio : IRepositorio<Categoria>
    {
        BlogContexto m_Context = null;

        public CategoriaRepositorio(BlogContexto context)
        {
            m_Context = context;
        }

        public IEnumerable<Categoria> GetTudo(System.Linq.Expressions.Expression<Func<Categoria, bool>> predicate = null)
        {
            return m_Context.Categorias.Where(predicate);
        }

        public Categoria Get(System.Linq.Expressions.Expression<Func<Categoria, bool>> predicate)
        {
            return m_Context.Categorias.SingleOrDefault(predicate);
        }

        public void Adicionar(Categoria entity)
        {
            m_Context.Categorias.Add(entity);
        }

        public void Atualizar(Categoria entity)
        {
            m_Context.Categorias.Attach(entity);
            ((IObjectContextAdapter)m_Context).ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
        }

        public void Deletar(Categoria entity)
        {
            m_Context.Categorias.Remove(entity);
        }

        public int Contar()
        {
            return m_Context.Categorias.Count();
        }
    }
}
