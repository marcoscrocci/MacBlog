using MacBlog.DAL.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacBlog.DAL.Repositorio
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositorio<Categoria> CategoriaRepositorio { get; }
        IRepositorio<Blog> BlogRepositorio { get; }
        IRepositorio<Comentario> ComentarioRepositorio { get; }
        IRepositorio<Usuario> UsuarioRepositorio { get; }
        IRepositorio<Perfil> PerfilRepositorio { get; }

        void Commit();
    }
}



