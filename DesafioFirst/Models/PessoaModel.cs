using DesafioFirst.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection.Metadata;

namespace DesafioFirst.Models
{
    public class PessoaModel {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string? Sobrenome { get; set; }

        public DateTime? dataNascimento { get; set; }

        public string Email { get; set; }

        public string? Telefone { get; set; }

        public string? Endereco { get; set; }

        public string? Cidade { get; set; }

        public string? Estado { get; set; }

        public string? Cep { get; set; }

        public string Cpf_cnpj { get; set; }

    }

}
