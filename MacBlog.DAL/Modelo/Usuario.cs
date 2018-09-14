using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MacBlog.DAL.Modelo
{
    public partial class Usuario
    {
        public Usuario()
        {
            Blogs = new HashSet<Blog>();
            Comentarios = new HashSet<Comentario>();
            Perfis = new HashSet<Perfil>();
        }

        public int UsuarioId { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Email Confirmado")]
        public bool EmailConfirmado { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Login")]
        public string NomeUsuario { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<Perfil> Perfis { get; set; }
    }
}
