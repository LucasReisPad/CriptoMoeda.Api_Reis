using Domain.Models;

namespace Domain.Adapters
{
    public interface IMercadoBitcoinAdapter
    {
        /// <summary>
        ///     Dados das últimas 24 horas de negociações de uma criptomoeda especifica.
        /// </summary>
        /// <param name="siglaMoeda">Sigla da criptomoeda que deseja obter dados.</param>
        /// <returns>
        ///     Informações com o resumo das últimas 24 horas de negociações.
        /// </returns>
        Task<NegociacoesDoDia> ObterDadosNegociacoesDoDiaAsync(string siglaMoeda);
    }
}
