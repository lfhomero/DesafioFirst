using AutoFixture;
using Moq;
using Xunit;
using DesafioFirst.Models;
using DesafioFirst.Repositories.Interfaces;
using DesafioFirst.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace DesafioFirst.UnitTest.Controllers
{
    public class PessoaControllerTest
    {
        
        [Fact]
        public void BuscarPessoasTest()
        {
            // Arrange
            //criar dados para o teste
            //var inputDados = new Fixture().Create<PessoaModel>();

            var pessoaRepositoryMock = new Mock<InterfacePessoaRepository>();
            var pessoaController = new PessoaController(pessoaRepositoryMock.Object);

            pessoaRepositoryMock.Setup(rm => rm.BuscarPessoas());

            // Act
            var resultado = pessoaController.BuscarPessoas();

            // Assert
            //verificando se o método retornou uma lista
            var lista = Assert.IsType<ActionResult<List<PessoaModel>>>(resultado.Result);
            //verificando se o retorno foi de sucesso HTTPStatus200
            Assert.IsType<OkObjectResult>(lista.Result);

            //verificando se o método foi chamado
            pessoaRepositoryMock.Verify(er => er.BuscarPessoas(), Times.Once);
        }

        [Fact]
        public void ErroBuscarPessoasPorIdTest()
        {
            // Arrange
            //criar dados para o teste
            var inputDadosBpid = new Fixture().Create<PessoaModel>();

            var pessoaRepositoryMock = new Mock<InterfacePessoaRepository>();

            pessoaRepositoryMock.Setup(rm => rm.BuscarPessoaPorId(inputDadosBpid.Id));

            var pessoaController = new PessoaController(pessoaRepositoryMock.Object);

            // Act

            var resultado = pessoaController.BuscarPessoaPorId(inputDadosBpid.Id);

            // Assert

            //verificando se o método retornou o id
            var id = Assert.IsType<ActionResult<List<PessoaModel>>>(resultado.Result);
            //verificando se o retorno foi de sucesso HTTPStatus200
            Assert.IsType<OkObjectResult>(id.Result);
            //Verificando se o Id criado é igual a 1, pelo fato de ser o primeiro cadastro simulado o id tem que ser igual a 1
            Assert.Equal(2, resultado.Id);
            //verificando se o método foi chamado
            pessoaRepositoryMock.Verify(er => er.BuscarPessoaPorId(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void AdicionarTest()
        {
            // Arrange
            //criar dados para o teste
            var inputDadosAdd = new Fixture().Create<PessoaModel>();

            var pessoaRepositoryMock = new Mock<InterfacePessoaRepository>();

            pessoaRepositoryMock.Setup(rm => rm.AdicionarPessoa(inputDadosAdd));

            var pessoaController = new PessoaController(pessoaRepositoryMock.Object);


            // Act
            var resultado = pessoaController.Adicionar(inputDadosAdd);

            // Assert
            //verificando se o método retornou uma Pessoa
            var id = Assert.IsType<ActionResult<PessoaModel>>(resultado.Result);
            //verificando se o retorno foi de sucesso HTTPStatus200
            Assert.IsType<OkObjectResult>(id.Result);

            //verificando se o método foi chamado
            pessoaRepositoryMock.Verify(er => er.AdicionarPessoa(It.IsAny<PessoaModel>()), Times.Once);
        }

        [Fact]
        public void AtualizarTest()
        {
            // Arrange
            //criar dados para o teste
            var inputDadosAtt = new Fixture().Create<PessoaModel>();
            var inputDados2Att = new Fixture().Create<PessoaModel>();

            var pessoaRepositoryMock = new Mock<InterfacePessoaRepository>();

            pessoaRepositoryMock.Setup(rm => rm.AtualizarPessoa(inputDados2Att,inputDadosAtt.Id));

            var pessoaController = new PessoaController(pessoaRepositoryMock.Object);


            // Act
            var resultado = pessoaController.Atualizar(inputDados2Att,inputDadosAtt.Id);

            // Assert
            //verificando se o método retornou uma Pessoa
            var id = Assert.IsType<ActionResult<PessoaModel>>(resultado.Result);
            //verificando se o retorno foi de sucesso HTTPStatus200
            Assert.IsType<OkObjectResult>(id.Result);

            //verificando se o método foi chamado
            pessoaRepositoryMock.Verify(er => er.AtualizarPessoa(It.IsAny<PessoaModel>(),It.IsAny<int>()), Times.Once);
        }
        [Fact]
        public void ErroDeletarTest()
        {
            // Arrange
            //criar dados para o teste
            var inputDadosDelErro = new Fixture().Create<PessoaModel>();

            var pessoaRepositoryMock = new Mock<InterfacePessoaRepository>();

            pessoaRepositoryMock.Setup(rm => rm.ApagarPessoa(inputDadosDelErro.Id));

            var pessoaController = new PessoaController(pessoaRepositoryMock.Object);

            // Act

            var resultado = pessoaController.Deletar(4);

            // Assert

            //verificando se o método retornou o id
            var id = Assert.IsType<ActionResult<PessoaModel>>(resultado.Result);
            //verificando se o retorno foi de sucesso HTTPStatus200
            Assert.IsType<OkObjectResult>(id.Result);
            //Verificando se o Id criado pelo Fixture é igual ao deletado pelo metodo
            Assert.Equal(10, resultado.Id);
            //verificando se o método foi chamado
            pessoaRepositoryMock.Verify(er => er.ApagarPessoa(It.IsAny<int>()), Times.Once);
        }
    }
}
