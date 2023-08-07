using Newtonsoft.Json;
using Refit;

namespace MercadoBitcoinAdapter.Clients
{
    public class NegociacoesDoDiaGetResult
    {
        /// <summary>
        /// Resultados das negociações das últimas 24 horas.
        /// </summary>
        [JsonProperty("ticker")]
        public ResultadosNegociacoesDoDiaGetResult? Resultados { get; set; }
    }

    public class ResultadosNegociacoesDoDiaGetResult
    {
        /// <summary>
        /// Maior preço unitário de negociação das últimas 24 horas.
        /// </summary>
        [JsonProperty("high")]
        public decimal MaiorPreco { get; set; }

        /// <summary>
        /// Menor preço unitário de negociação das últimas 24 horas.
        /// </summary>
        [JsonProperty("low")]
        public decimal MenorPreco { get; set; }

        /// <summary>
        /// Quantidade negociada nas últimas 24 horas.
        /// </summary>
        [JsonProperty("vol")]
        public decimal QuantidadeNegociada { get; set; }

        /// <summary>
        /// Preço unitário da última negociação.
        /// </summary>
        [JsonProperty("last")]
        public decimal PrecoUnitario { get; set; }

        /// <summary>
        /// Maior preço de oferta de compra das últimas 24 horas.
        /// </summary>
        [JsonProperty("buy")]
        public decimal MaiorPrecoOfertado { get; set; }

        /// <summary>
        /// Menor preço de oferta de venda das últimas 24 horas.
        /// </summary>
        [JsonProperty("sell")]
        public decimal MenorPrecoOfertado { get; set; }

        /// <summary>
        /// Data e hora da informação em Era Unix.
        /// </summary>
        [JsonProperty("date")]
        public int DataHora { get; set; }
    }
}
