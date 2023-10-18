using DesafioFirst.Data;
using DesafioFirst.Models;
using DesafioFirst.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Net.Mail;

namespace DesafioFirst.Repositories
{
    public class PessoaRepository : InterfacePessoaRepository
    {
        private readonly DesafioFirstDB _dbContext;

        public PessoaRepository(DesafioFirstDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PessoaModel> AdicionarPessoa(PessoaModel pessoa)
        {
                   
            bool validaCPF = ValidaCPF(pessoa.Cpf_cnpj);
            bool validaCNPJ = ValidaCNPJ(pessoa.Cpf_cnpj);
            bool validaEmail = ValidaEmail(pessoa.Email);

            if ((validaCPF == true || validaCNPJ == true) && (validaEmail == true))
            {
                await _dbContext.Pessoas.AddAsync(pessoa);
                await _dbContext.SaveChangesAsync();
            }
            else if ((validaCPF == true || validaCNPJ == true) && (validaEmail == false))
            {
                throw new Exception($"Não foi informado um Email válido: {pessoa.Email}");
            }
            else
            {
                throw new Exception($"Não foi informado um CPF ou CNPJ válido: {pessoa.Cpf_cnpj}");
            }
        
            return pessoa;
        }

        public async Task<bool> ApagarPessoa(int id)
        {
            //reutilizando o método criado para buscar uma pessoa por id
            PessoaModel pessoaPorId = await BuscarPessoaPorId(id);

            //verificando se retornou dados na consulta por id
            if (pessoaPorId == null)
            {
                throw new Exception($"Pessoa para o ID: {id} não foi encontrada no banco de dados.");                
            }

            _dbContext.Pessoas.Remove(pessoaPorId);
            await _dbContext.SaveChangesAsync();
            
            return true;
        }

        public async Task<PessoaModel> AtualizarPessoa(PessoaModel pessoa, int id)
        {
            //reutilizando o método criado para buscar uma pessoa por id
            PessoaModel pessoaPorId = await BuscarPessoaPorId(id);

            //verificando se retornou dados na consulta por id
            if(pessoaPorId == null)
            {
                throw new Exception($"Pessoa para o ID: {id} não foi encontrada no banco de dados.");
            }

            bool validaCPF = ValidaCPF(pessoa.Cpf_cnpj);
            bool validaCNPJ = ValidaCNPJ(pessoa.Cpf_cnpj);
            bool validaEmail = ValidaEmail(pessoa.Email);

            if ((validaCPF == true || validaCNPJ == true) && (validaEmail == true))
            {

            pessoaPorId.Nome = pessoa.Nome;
            pessoaPorId.Sobrenome = pessoa.Sobrenome;
            pessoaPorId.dataNascimento = pessoa.dataNascimento;
            pessoaPorId.Email = pessoa.Email;
            pessoaPorId.Telefone = pessoa.Telefone;
            pessoaPorId.Endereco = pessoa.Endereco;
            pessoaPorId.Cidade = pessoa.Cidade;
            pessoaPorId.Estado = pessoa.Estado;
            pessoaPorId.Cep = pessoa.Cep;
            pessoaPorId.Cpf_cnpj = pessoa.Cpf_cnpj;

            _dbContext.Pessoas.Update(pessoaPorId);
            await _dbContext.SaveChangesAsync();

            }
            else if((validaCPF == true || validaCNPJ == true) && (validaEmail == false))
            {
                throw new Exception($"Não foi informado um Email válido: {pessoa.Email}");
            } else {
                throw new Exception($"Não foi informado um CPF ou CNPJ válido: {pessoa.Cpf_cnpj}");
            }

            return pessoaPorId;

        }

        public async Task<PessoaModel> BuscarPessoaPorId(int id)
        {
            //await utilizado para aguardar o retorno da consulta Async
            return await _dbContext.Pessoas.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<PessoaModel>> BuscarPessoas()
        {
            return await _dbContext.Pessoas.ToListAsync();
        }

        public bool ValidaCPF(string vrCPF)

        {

            string valor = vrCPF.Replace(".", "");
            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return false;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(
                  valor[i].ToString());

            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];
            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }

            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];
            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)

            {
                if (numeros[10] != 0)
                    return false;
            }

            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;

        }
        public bool ValidaCNPJ(string vrCNPJ)

        {
            string CNPJ = vrCNPJ.Replace(".", "");
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", "");

            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;

            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;


            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                        CNPJ.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig + 1, 1)));
                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] *
                          int.Parse(ftmt.Substring(
                          nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (
                        resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == 0);
                    else
                        CNPJOk[nrDig] = (
                        digitos[12 + nrDig] == (
                        11 - resultado[nrDig]));
                }
                return (CNPJOk[0] && CNPJOk[1]);
            }
            catch
            {
                return false;
            }
        }
        public bool ValidaEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
    }
