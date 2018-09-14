using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacBlog.DAL.Modelo;
using System.ComponentModel.DataAnnotations;

namespace MacBlog.Web.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Login")]
        [Required(ErrorMessage = "Informe a login do usuário")]
        public string Login { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Informe o email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
