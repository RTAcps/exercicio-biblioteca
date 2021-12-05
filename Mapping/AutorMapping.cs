using ExercicioBiblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExercicioBiblioteca.Mapping
{
    public class AutorMapping : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.HasKey(x => x.Codigo);
            builder.Property(x => x.Nome).IsRequired();
            builder.ToTable("autores");
        }
    }
}
