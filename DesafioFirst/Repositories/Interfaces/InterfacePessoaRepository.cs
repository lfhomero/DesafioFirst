using DesafioFirst.Models;

namespace DesafioFirst.Repositories.Interfaces
{
    public interface InterfacePessoaRepository
    {
        Task<List<PessoaModel>> BuscarPessoas();

        Task<PessoaModel> BuscarPessoaPorId(int id);

        Task<PessoaModel> AdicionarPessoa(PessoaModel pessoa);

        Task<PessoaModel> AtualizarPessoa(PessoaModel pessoa, int id);

        Task<bool> ApagarPessoa(int id);

    }
}