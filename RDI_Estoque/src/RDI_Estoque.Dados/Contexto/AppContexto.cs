using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RDI_Estoque.Dados.EntidadeConfig;
using RDI_Estoque.Dominio.Entidades;

namespace RDI_Estoque.Dados.Contexto
{
    public class AppContexto : IdentityDbContext
    {
        public AppContexto() { }
        public AppContexto(DbContextOptions<AppContexto> opt) : base(opt) { Database.EnsureCreated(); }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder
                    .UseSqlServer("Data Source=(localdb)\\local;Initial Catalog=RickLocalization;persist security info=True;Integrated Security=SSPI;");
        }
        public DbSet<NotaFiscal> NotasFiscais { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProdutoConfig());
            modelBuilder.ApplyConfiguration(new NotaFiscalConfig());
        }


    }
}
