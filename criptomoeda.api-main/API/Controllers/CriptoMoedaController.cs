using AutoMapper;
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
    }
}
