using ExercicioBiblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExercicioBiblioteca.Mapping
{
    public class LivroMapping : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.HasKey(x => x.Codigo);
            builder.Property(x => x.Descricao).HasColumnType("VARCHAR(60").IsRequired();
            builder.Property(x => x.ISBN).IsRequired();
            builder.Property(x => x.AnoLancamento).IsRequired();
            builder.Property(x => x.CodigoEditora).IsRequired(false);
            builder.HasOne(x => x.Autor)
                .WithMany()
                .HasForeignKey(x => x.CodigoAutor);
            builder.ToTable("livros");
        }
    }
}
