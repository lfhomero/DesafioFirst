using DesafioFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioFirst.Data.Map
{
    public class PessoaMap : IEntityTypeConfiguration<PessoaModel>
    {
        public void Configure(EntityTypeBuilder<PessoaModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Sobrenome).HasMaxLength(255);
            builder.Property(x => x.dataNascimento);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Telefone).HasMaxLength(20);
            builder.Property(x => x.Endereco).HasMaxLength(255);
            builder.Property(x => x.Cidade).HasMaxLength(100);
            builder.Property(x => x.Estado).HasMaxLength(50);
            builder.Property(x => x.Cep).HasMaxLength(10);
            builder.Property(x => x.Cpf_cnpj).IsRequired().HasMaxLength(14);

        }
    }
}
