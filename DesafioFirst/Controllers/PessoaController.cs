using DesafioFirst.Models;
using DesafioFirst.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly InterfacePessoaRepository _pessoaRepository;

        public PessoaController(InterfacePessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<PessoaModel>>> BuscarPessoas()
        {
            List<PessoaModel> pessoas = await _pessoaRepository.BuscarPessoas();
            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<PessoaModel>>> BuscarPessoaPorId(int id)
        {
            PessoaModel pessoaPorId = await _pessoaRepository.BuscarPessoaPorId(id);
            if (pessoaPorId == null)
            {            
                    throw new Exception($"Pessoa para o ID: {id} não foi encontrada no banco de dados.");
            }
            return Ok(pessoaPorId);
        }

        [HttpPost]
        public async Task<ActionResult<PessoaModel>> Adicionar([FromBody] PessoaModel pessoaModel)
        {
           PessoaModel pessoa = await _pessoaRepository.AdicionarPessoa(pessoaModel);
           return Ok(pessoa);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PessoaModel>> Atualizar([FromBody] PessoaModel pessoaModel, int id)
        {
            pessoaModel.Id = id;
            PessoaModel pessoa = await _pessoaRepository.AtualizarPessoa(pessoaModel, id);
            return Ok(pessoa);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<PessoaModel>> Deletar(int id)
        {
            bool deletado = await _pessoaRepository.ApagarPessoa(id);
            return Ok(deletado);
        }
    }
}
