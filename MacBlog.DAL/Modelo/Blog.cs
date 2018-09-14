using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MacBlog.DAL.Modelo
{
    public partial class Blog
    {
        public Blog()
        {
            Comentarios = new HashSet<Comentario>();
        }

        public int BlogId { get; set; }
        [Required]
        [StringLength(150)]
        public string Titulo{ get; set; }

        [Required]
        [Display(Name = "Conteúdo")]
        public string Corpo { get; set; }

        [Display(Name = "Criado Em")]
        public DateTime CriadoEm { get; set; }

        [Display(Name = "Atualizado Em")]
        public DateTime AtualizadoEm { get; set; }

        public int CategoriaId { get; set; }
        public int AutorId { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
    }
}
