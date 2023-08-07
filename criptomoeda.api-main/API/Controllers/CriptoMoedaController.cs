using AutoMapper;
using Domain.Models;
using Domain.Services;
using Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CriptoMoeda.Api.Controllers
{
    [Route("[controller]")]
    public class CriptoMoedaController : ControllerBase
    {
        private readonly ICriptoMoedaService criptoMoedaService;
        private readonly IMapper mapper;

        public CriptoMoedaController(ICriptoMoedaService criptoMoedaService, IMapper mapper)
        {
            this.criptoMoedaService = criptoMoedaService ??
                throw new ArgumentNullException(nameof(criptoMoedaService));

            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        ///     Dados das últimas 24 horas de negociações de uma criptomoeda especifica.
        /// </summary>
        /// <param name="siglaMoeda">
        ///     Sigla da criptomoeda que deseja obter dados.
        /// </param>
        [ProducesResponseType(typeof(NegociacoesDoDiaGetResult), 200)]
        /// <response code="400"> Dados inválidos</response>
        /// <response code="500">Erro interno.</response>
        [HttpGet("ObterDadosNegociacoesDoDia")]
        public async Task<IActionResult> ObterDadosNegociacoesDoDiaAsync(string siglaMoeda)
        {
            var resultado = await criptoMoedaService.
                ObterDadosNegociacoesDoDiaAsync(siglaMoeda);

            return Ok(mapper.Map<NegociacoesDoDiaGetResult>(resultado));
        }

        /// <summary>
        ///     Adiciona Dados das últimas 24 horas de negociações de uma criptomoeda especifica no banco.
        /// </summary>
        /// <param name="siglaMoeda">
        ///     Sigla da criptomoeda que deseja obter dados.
        /// </param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        /// <response code="400"> Dados inválidos</response>
        /// <response code="500">Erro interno.</response>
        /// <response code="204"> No Content</response>
        [HttpPost("AddDadosMoeda")]
        public async Task<IActionResult> AddPostMoedaAsync(string siglaMoeda)
        {
            if (siglaMoeda == null)
            {
                return BadRequest();
            }
            var resultado = await criptoMoedaService.
                ObterDadosNegociacoesDoDiaAsync(siglaMoeda);

            var coinData = mapper.Map<CoinData>(resultado);
            coinData.Coin = siglaMoeda;

            var SearchData = mapper.Map<CoinDataRegisterSearch>(resultado);
            SearchData.Coin = siglaMoeda;

            await criptoMoedaService.UpsertCoinDataAsync(coinData, SearchData);
            

            return NoContent();
        }

        /// <summary>
        ///     Dados das últimas 24 horas de negociações de uma criptomoeda especifica.
        /// </summary>
        /// <param name="siglaMoeda">
        ///     Sigla da criptomoeda que deseja obter dados.
        /// </param>
        [ProducesResponseType(typeof(IEnumerable<CoinDataRegisterSearch>), 200)]
        /// <response code="400"> Dados inválidos</response>
        /// <response code="500">Erro interno.</response>
        [HttpGet("GetListaHistóricoDeBusca")]
        public async Task<ActionResult<IEnumerable<CoinDataRegisterSearch>>> GeTListHistoricoDeBuscaAsync(string siglaMoeda)
        {
            if (siglaMoeda == null)
            {
                return BadRequest();
            }
            var resultado = await criptoMoedaService.
                GetCoinDataRegisterSearchAsync(siglaMoeda);

            if (resultado == null)
            {
                return NotFound();
            }

            return Ok(resultado);
        }

        /// <summary>
        ///     Dados das Atualizados do banco de dados de uma criptomoeda especifica.
        /// </summary>
        /// <param name="siglaMoeda">
        ///     Sigla da criptomoeda que deseja obter dados.
        /// </param>
        [ProducesResponseType(typeof(CoinData), 200)]
        /// <response code="400"> Dados inválidos</response>
        /// <response code="500">Erro interno.</response>
        [HttpGet("ObterMoedaAtualizada")]
        public async Task<ActionResult<CoinData>> GeTCoinDataAtt(string siglaMoeda)
        {
            if (siglaMoeda == null)
            {
                return BadRequest();
            }
            var resultado = await criptoMoedaService.
                GetCoinDataAtt(siglaMoeda);

            if (resultado == null)
            {
                return NotFound();
            }

            return Ok(resultado);
        }
    }
}