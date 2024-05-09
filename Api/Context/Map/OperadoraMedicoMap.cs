using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProjetoPloomes.Models;

namespace ApiPloomes.Context.Map
{
    public class OperadoraMedicoMap : IEntityTypeConfiguration<OperadoraMedico>
    {
        public void Configure(EntityTypeBuilder<OperadoraMedico> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.OperadoraId).IsRequired();
            builder.Property(x => x.MedicoId).IsRequired();
        }
    }
}
