using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoPloomes.Models;

namespace ApiPloomes.Context.Map
{
    public class EmpresaMap : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Telefone).HasMaxLength(16).IsRequired();
            builder.Property(x => x.Cnpj).HasMaxLength(18).IsFixedLength().IsRequired();
        }
    }
}
