using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProjetoPloomes.Models;

namespace ApiPloomes.Context.Map
{
    public class OperadoraMap : IEntityTypeConfiguration<Operadora>
    {
        public void Configure(EntityTypeBuilder<Operadora> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasMaxLength(200).IsRequired();
        }
    }
}
