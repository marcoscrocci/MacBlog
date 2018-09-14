using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MacBlog.DAL.Repositorio;
using MacBlog.DAL.Modelo;

namespace MacBlog.Web.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Index()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                List<Categoria> categorias = uow.CategoriaRepositorio.GetTudo().ToList();
                return View(categorias);
            }
        }

        //
        // GET: /Categoria/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Categoria/Create
        [HttpPost]
        public ActionResult Create(Categoria model)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.CategoriaRepositorio.Adicionar(model);
                    uow.Commit();
                    TempData["mensagem"] = string.Format("{0}  : foi incluído com sucesso", model.CategoriaNome);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Categoria categoria = uow.CategoriaRepositorio.Get(item => item.CategoriaId == id);

                return View(categoria);
            }
        }

        //
        // POST: /Category/Edit/5
        [HttpPost]
        public ActionResult Edit(Categoria model)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    uow.CategoriaRepositorio.Atualizar(model);
                    uow.Commit();
                    TempData["mensagem"] = string.Format("{0}  : foi alterada com sucesso", model.CategoriaNome);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Category/Delete/5
        public ActionResult Delete(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Categoria categoria = uow.CategoriaRepositorio.Get(item => item.CategoriaId == id);
                return View(categoria);
            }
        }

        //
        // POST: /Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    Categoria categoria = uow.CategoriaRepositorio.Get(item => item.CategoriaId == id);
                    uow.CategoriaRepositorio.Deletar(categoria);
                    uow.Commit();
                    TempData["mensagem"] = string.Format("{0}  : foi deletada com sucesso.",  categoria.CategoriaNome);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}