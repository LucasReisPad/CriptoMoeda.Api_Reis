using Refit;

namespace MercadoBitcoinAdapter.Clients
{
    public interface IMercadoBitcoinApi
    {
        [Get("/{siglaCripto}/ticker/")]
        Task<NegociacoesDoDiaGetResult> ObterNegociacoesDoDiaAsync(
            string siglaCripto);
    }
}
