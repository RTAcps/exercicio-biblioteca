using ExercicioBiblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExercicioBiblioteca.Mapping
{
    public class ItensEmprestimoMapping : IEntityTypeConfiguration<ItensEmprestimo>
    {
        public void Configure(EntityTypeBuilder<ItensEmprestimo> builder)
        {
            builder.HasKey(x => x.CodigoItem);
            builder.HasOne(x => x.Emprestimo)
                .WithMany(x => x.Itens)
                .HasForeignKey(x => x.NumeroEmprestimo);
            builder.HasOne(x => x.Livro)
                .WithMany()
                .HasForeignKey(x => x.CodigoLivro);
            builder.ToTable("itens_emprestimos");
        }
    }
}
