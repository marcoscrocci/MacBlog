using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MacBlog.DAL.Modelo
{
    public partial class Categoria
    {
        public Categoria()
        {
            Blogs = new HashSet<Blog>();
        }

        public int CategoriaId { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name ="Nome da Categoria")]
        public string CategoriaNome { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
