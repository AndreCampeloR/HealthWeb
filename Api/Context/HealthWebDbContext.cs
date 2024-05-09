using ApiPloomes.Context.Map;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoPloomes.Models;

namespace ApiPloomes.Context
{
    public class HealthWebDbContext : DbContext
    {
        public HealthWebDbContext(DbContextOptions<HealthWebDbContext> options) : base(options)
        {
        }

        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Operadora> Operadora { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<OperadoraMedico> OperadoraMedico { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EmpresaMap());
            builder.ApplyConfiguration(new OperadoraMap());
            builder.ApplyConfiguration(new MedicoMap());
            builder.ApplyConfiguration(new OperadoraMedicoMap());
            base.OnModelCreating(builder);
        }

    }
}
