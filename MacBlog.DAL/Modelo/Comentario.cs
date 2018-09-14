using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MacBlog.DAL.Modelo
{
    public partial class Comentario
    {
        public int ComentarioId { get; set; }

        [Display(Name = "Comentário")]
        public string Corpo { get; set; }

        [Display(Name = "Data de criação")]
        public DateTime? CriadoEm { get; set; }

        public int? BlogId { get; set; }
        public int? PostId { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
