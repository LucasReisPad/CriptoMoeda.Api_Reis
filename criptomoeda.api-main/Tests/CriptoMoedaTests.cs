using Domain.Adapters;
using Domain.Models;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace Application.Tests
{
    [TestClass]
    public class CriptoMoedaTests
    {
        private readonly ICriptoMoedaService criptoMoedaService;
        private readonly Mock<IMercadoBitcoinAdapter> mercadoBitcoinAdapterMock;

        public CriptoMoedaTests()
        {
            mercadoBitcoinAdapterMock = new Mock<IMercadoBitcoinAdapter>();
            criptoMoedaService = new CriptoMoedaService(mercadoBitcoinAdapterMock.Object);
        }

        [TestMethod]
        [Trait(nameof(ICriptoMoedaService.ObterDadosNegociacoesDoDiaAsync), "Sucesso")]
        public async Task ObterDadosNegociacoesDoDiaAsync_Sucesso()
        {
            var siglaCripto = "xpto";

            var retornoAtualizarIntegracao = new NegociacoesDoDia()
            {
                DataHora = DateTime.Parse("2010-10-10"),
                MaiorPreco = 10,
                MenorPreco = 5,
                MaiorPrecoOfertado = 11,
                MenorPrecoOfertado = 4,
                PrecoUnitario = 10,
                QuantidadeNegociada = 100
            };

            mercadoBitcoinAdapterMock
                .Setup(m => m.ObterDadosNegociacoesDoDiaAsync(It.IsAny<string>()))
                .Callback<string>(
                (sigla) =>
                {
                    Assert.Equal("xpto", sigla);
                })
                .ReturnsAsync(retornoAtualizarIntegracao);


            await criptoMoedaService.ObterDadosNegociacoesDoDiaAsync(siglaCripto);

            mercadoBitcoinAdapterMock.Verify(s => s.
            ObterDadosNegociacoesDoDiaAsync(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [Trait(nameof(ICriptoMoedaService.ObterDadosNegociacoesDoDiaAsync), "Erro")]
        public async Task ObterDadosNegociacoesDoDiaAsync_Erro()
        {
            await Xunit.Assert.ThrowsAnyAsync<Exception>(async () =>
            {
                await criptoMoedaService.ObterDadosNegociacoesDoDiaAsync(string.Empty);
            });
        }
    }
}
