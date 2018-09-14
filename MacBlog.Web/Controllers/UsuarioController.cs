using System.Web.Mvc;
using MacBlog.Web.Models;
using System.Web.Security;
using MacBlog.DAL.Modelo;
using MacBlog.DAL.Repositorio;

namespace MacBlog.Web.Controllers
{
    public class UsuarioController : Controller
    {
      
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        var usuario = uow.UsuarioRepositorio.Get(item => item.Senha == model.Senha && item.NomeUsuario == model.Login);
                        if (usuario != null)
                        {
                            Session.Add("usuario", usuario);
                            Session.Add("nomeusuario", usuario.NomeUsuario);
                            FormsAuthentication.SetAuthCookie(usuario.NomeUsuario,true);
                            return RedirectToAction("Index", "Home", null);
                        }
                        else
                        {
                            ViewBag.Message = "Login inválido!";
                        }
                    }
                }
                catch 
                {
                    ViewBag.Message = "Login falhou !";
                }
            }
            return View(model);
        }

        //GET
        public ActionResult Register()
        {
            Usuario u = new Usuario();
            return View(u);
        }

        [HttpPost]
        public ActionResult Register(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (UnitOfWork uow = new UnitOfWork())
                    {
                        uow.UsuarioRepositorio.Adicionar(usuario);
                        uow.Commit();
                        return RedirectToAction("Login");
                    }
                }
                catch
                {
                    ViewBag.Message = "Registro falhou !";
                }
            }
            return View(usuario);
        }

        public ActionResult Exists(string email)
        {
           using (UnitOfWork uow = new UnitOfWork())
           {
              var usuario = uow.UsuarioRepositorio.Get(item => item.Email == email);
              if (usuario == null)
              {
                   return Content("");
              }
              else
              {
                  ViewBag.Message = "Email ja existe!";
                  return Content("Email já existe !");
             }
           }
        }

        [Authorize]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        //[Authorize]
        //public ActionResult ChangePassword()
        //{
        //    var model = new ChangePasswordViewModel();
        //    return View(model);
        //}

        //[HttpPost]
        //[Authorize]
        //public ActionResult ChangePassword(ChangePasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            depositsEntities ctx = new depositsEntities();
        //            User cuser = Session["user"] as User;
        //            var dbuser = ctx.Users.Where(u => u.UserId == cuser.UserId).SingleOrDefault();

        //            if (dbuser.Password != model.OldPassword)
        //                ViewBag.Message = "Senha inválida !";
        //            else
        //            {
        //                dbuser.Password = model.NewPassword;
        //                ctx.SaveChanges();
        //                ViewBag.Message = "Senha alterada com sucesso!";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Utility.WriteToTrace(ex);
        //            ViewBag.Message = "Não foi possível alterar a senha!";
        //        }
        //    }

        //    return View(model);
        //}

    }
}