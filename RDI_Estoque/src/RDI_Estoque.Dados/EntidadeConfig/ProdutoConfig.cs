using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RDI_Estoque.Dominio.Entidades;

namespace RDI_Estoque.Dados.EntidadeConfig
{
    public class ProdutoConfig : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome).HasColumnName("Descricao").IsRequired();
            //builder.HasOne(s => s.Rick)
            //    .WithMany(x => x.Dimensoes).HasForeignKey(x => x.Id);
        }
    }
}
