using MacBlog.DAL.Modelo;
using MacBlog.DAL.Repositorio;
using MacBlog.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MacBlog.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly UnitOfWork uow;

        public BlogController()
        {
            uow = new UnitOfWork();
        }

        public BlogController(UnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }

        // GET: /Blogs/
        public ActionResult Index(int? pagina)
        {
                IEnumerable<Blog> blogs = uow.BlogRepositorio.GetTudo();
                blogs = blogs.OrderByDescending(blog => blog.AtualizadoEm);
                IEnumerable<BlogResumoModel> lista = from blog in blogs
                                                     select new BlogResumoModel
                                                     {
                                                         Id = blog.BlogId,
                                                         Titulo = blog.Titulo,
                                                         Resumo = blog.Corpo.Length > 200 ? blog.Corpo.Substring(0, 200) : blog.Corpo,
                                                         NomeAutor = blog.Usuario.NomeUsuario,
                                                         ComentariosQuantidade = blog.Comentarios.Count(),
                                                         CriadoEm = blog.CriadoEm,
                                                         AtualizadoEm = blog.AtualizadoEm
                                                     };
                return View(lista.ToList().ToPagedList(pagina ?? 1, 2));
        }

        // GET: /Blogs/Details/5
        public ActionResult Details(int id)
        {
            Blog blog = uow.BlogRepositorio.Get(item => item.BlogId == id);
            return View(blog);
        }

        // GET: /Blogs/Create
        public ActionResult Create()
        {
           List<Categoria> categorias = uow.CategoriaRepositorio.GetTudo().ToList();
           ViewBag.CategoriaLista = categorias;
            return View();
        }

        //
        // POST: /Blogs/Create
        [HttpPost]
        public ActionResult Create(Blog model)
        {
            try
            {
                    model.CategoriaId = Convert.ToInt32(Request.Form["CategoriaId"]);
                    model.CriadoEm = DateTime.Now;
                    model.AtualizadoEm = DateTime.Now;
                    model.AutorId = uow.UsuarioRepositorio.Get(user => user.NomeUsuario == User.Identity.Name).UsuarioId;

                    uow.BlogRepositorio.Adicionar(model);
                    uow.Commit();
                    TempData["mensagem"] = string.Format("{0}  : foi incluído com sucesso", model.Titulo.Substring(0,20));
                return RedirectToAction("Index");
            }
            catch
            {
                    TempData["mensagem"] = string.Format("Erro ao criar o blog. !");
                    List<Categoria> categorias = uow.CategoriaRepositorio.GetTudo().ToList();
                    ViewBag.CategoriaLista = categorias;
                    return View();
            }
        }

        //
        // GET: /Blogs/Edit/5
        public ActionResult Edit(int id)
        {
                Blog blog = uow.BlogRepositorio.Get(item => item.BlogId == id);

                List<Categoria> categorias = uow.CategoriaRepositorio.GetTudo().ToList();
                ViewBag.CategoriaLista = categorias;
                return View(blog);
        }

        //
        // POST: /Blogs/Edit/5
        [HttpPost]
        public ActionResult Edit(Blog model)
        {
            try
            {
                model.CategoriaId = Convert.ToInt32(Request.Form["CategoriaId"]);
                model.AtualizadoEm = DateTime.Now;

                uow.BlogRepositorio.Atualizar(model);
                uow.Commit();
                TempData["mensagem"] = string.Format("Blog alterado com sucesso !");
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["mensagem"] = string.Format("{0}  : Erro ao atualizar o Blog");
                List<Categoria> categorias = uow.CategoriaRepositorio.GetTudo().ToList();
                ViewBag.CategoriaLista = categorias;
                return View();
            }
        }

        //
        // GET: /Blogs/Delete/5
        public ActionResult Delete(int id)
        {
                Blog blog = uow.BlogRepositorio.Get(item => item.BlogId == id);
                return View(blog);
        }

        //
        // POST: /Blogs/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                    Blog blog = uow.BlogRepositorio.Get(item => item.BlogId == id);
                    uow.BlogRepositorio.Deletar(blog);
                    uow.Commit();
                    TempData["mensagem"] = string.Format("{0}  : foi deletado com sucesso", blog.Titulo.Substring(0, 20));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult CreateComment(int blogId)
        {
                Comentario comentario = new Comentario();
                comentario.BlogId = blogId;
            return View();
        }

        //
        // POST: /Blogs/Create
        [HttpPost]
        public ActionResult CreateComment(Comentario model)
        {
            try
            {
                    model.CriadoEm = DateTime.Now;
                    model.PostId = uow.UsuarioRepositorio.Get(user => user.NomeUsuario == User.Identity.Name).UsuarioId;

                    uow.ComentarioRepositorio.Adicionar(model);
                    uow.Commit();
                    TempData["mensagem"] = string.Format("O Comentário foi incluído com sucesso");
                return RedirectToAction("Details", new { id = model.BlogId });
            }
            catch
            {
                    List<Categoria> categorias = uow.CategoriaRepositorio.GetTudo().ToList();
                    ViewBag.CategoriaLista = categorias;
                    return View();
            }
        }

        public ActionResult DeleteComment(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteComment(int id, FormCollection collection)
        {
            try
            {
                    Comentario comentario = uow.ComentarioRepositorio.Get(item => item.ComentarioId == id);
                    uow.ComentarioRepositorio.Deletar(comentario);
                    uow.Commit();
                    TempData["mensagem"] = string.Format("O Comentário foi excluído com sucesso");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}