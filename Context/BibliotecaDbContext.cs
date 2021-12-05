using ExercicioBiblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace ExercicioBiblioteca.Context
{
    public class BibliotecaDbContext : DbContext
    {
        #region DbSets
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Leitor> Leitores { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<ItensEmprestimo> ItensEmprestimos { get; set; }

        #endregion

        #region Constructor
        public BibliotecaDbContext(DbContextOptions options) : base(options)
        {
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BibliotecaDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
