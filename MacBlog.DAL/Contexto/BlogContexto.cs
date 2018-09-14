using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacBlog.DAL.Modelo;

namespace MacBlog.DAL.Contexto
{
    public partial class BlogContexto : DbContext
    {
        public BlogContexto()
                : base("name=ConexaoMacBlog")
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Comentario> Comentarios { get; set; }
        public virtual DbSet<Perfil> Perfis { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // relacionamento um para muitos entre Categoria e Blog
            modelBuilder.Entity<Categoria>()
            .HasMany(e => e.Blogs)
            .WithRequired(e => e.Categoria)
            .HasForeignKey(e => e.CategoriaId)
            .WillCascadeOnDelete(false);

            // relacionamento um para muiots entre Blog e Comentario
            modelBuilder.Entity<Blog>()
            .HasMany(e => e.Comentarios)
            .WithRequired(e => e.Blog)
            .HasForeignKey(e => e.BlogId)
            .WillCascadeOnDelete(false);

            // relacionamento um para muitos entre Usuario e Blog
            modelBuilder.Entity<Usuario>()
            .HasMany(e => e.Blogs)
            .WithRequired(e => e.Usuario)
            .HasForeignKey(e => e.AutorId)
            .WillCascadeOnDelete(false);

            // relacionamento um para muitos entre Usuario e Comentario
            modelBuilder.Entity<Usuario>()
            .HasMany(e => e.Comentarios)
            .WithOptional(e => e.Usuario)
            .HasForeignKey(e => e.PostId);

            // Relacionamento muuitos para muitos entre Usuario e Perfil
            modelBuilder.Entity<Usuario>()
            .HasMany(e => e.Perfis)
            .WithMany(e => e.Usuarios)
            .Map(m => m.ToTable("UsuarioPerfis")
                .MapLeftKey("UsuarioId")
                    .MapRightKey("PerfilId"));
        }
    }
}
