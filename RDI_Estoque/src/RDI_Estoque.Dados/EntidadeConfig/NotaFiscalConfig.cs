using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RDI_Estoque.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDI_Estoque.Dados.EntidadeConfig
{
    public class NotaFiscalConfig : IEntityTypeConfiguration<NotaFiscal>
    {
        public void Configure(EntityTypeBuilder<NotaFiscal> builder)
        {
            builder.ToTable("NotaFiscal");
            builder.HasKey(c => new { c.Id, c.ChaveAcessoNfe });
            builder.Property(c => c.Nome).HasColumnName("TipoNota").IsRequired();
            builder.Property(c => c.IdUsuarioCadastro).IsRequired();
            builder.HasOne(s => s.Produto)
                .WithMany(x => x.NotasFiscais).HasForeignKey(x => x.IdProduto);
        }
    }
}
