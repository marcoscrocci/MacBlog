using MacBlog.DAL.Contexto;
using MacBlog.DAL.Modelo;
using System;

namespace MacBlog.DAL.Repositorio
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private BlogContexto _contexto = null;
        private Repositorio<Categoria> categoriaRepositorio = null;
        private Repositorio<Blog> blogRepositorio = null;
        private Repositorio<Comentario> comentarioRepositorio = null;
        private Repositorio<Usuario> usuarioRepositorio = null;
        private Repositorio<Perfil> perfilRepositorio = null;

        public UnitOfWork()
        {
            _contexto = new BlogContexto();
        }

        public void Commit()
        {
            _contexto.SaveChanges();
        }

        public IRepositorio<Categoria> CategoriaRepositorio
        {
            get
            {
                if (categoriaRepositorio == null)
                {
                    categoriaRepositorio = new Repositorio<Categoria>(_contexto);
                }
                return categoriaRepositorio;
            }
        }

        public IRepositorio<Blog> BlogRepositorio
        {
            get
            {
                if (blogRepositorio == null)
                {
                    blogRepositorio = new Repositorio<Blog>(_contexto);
                }
                return blogRepositorio;
            }
        }

        public IRepositorio<Comentario> ComentarioRepositorio
        {
            get
            {
                if (comentarioRepositorio == null)
                {
                    comentarioRepositorio = new Repositorio<Comentario>(_contexto);
                }
                return comentarioRepositorio;
            }
        }

        public IRepositorio<Usuario> UsuarioRepositorio
        {
            get
            {
                if (usuarioRepositorio == null)
                {
                    usuarioRepositorio = new Repositorio<Usuario>(_contexto);
                }
                return usuarioRepositorio;
            }
        }

        public IRepositorio<Perfil> PerfilRepositorio
        {
            get
            {
                if (perfilRepositorio == null)
                {
                    perfilRepositorio = new Repositorio<Perfil>(_contexto);
                }
                return perfilRepositorio;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _contexto.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
