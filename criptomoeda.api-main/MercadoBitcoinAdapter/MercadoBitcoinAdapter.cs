using AutoMapper;
using Domain.Adapters;
using Domain.Models;
using MercadoBitcoinAdapter.Clients;
using Refit;

namespace Adapter.MercadoBitcoinAdapter
{
    public class MercadoBitcoinAdapter : IMercadoBitcoinAdapter
    {
        private readonly IMapper mapper;
        private readonly IMercadoBitcoinApi mercadoBitcoinApi;

        public MercadoBitcoinAdapter(IMercadoBitcoinApi mercadoBitcoinApi,
                            IMapper mapper)
        {
            this.mercadoBitcoinApi = mercadoBitcoinApi ??
                throw new ArgumentNullException(nameof(mercadoBitcoinApi));

            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<NegociacoesDoDia> ObterDadosNegociacoesDoDiaAsync(string siglaMoeda)
        {
            try 
            {
                var resultadoNegociacoes = await mercadoBitcoinApi.
                    ObterNegociacoesDoDiaAsync(siglaMoeda);

                return mapper.Map<NegociacoesDoDia>(resultadoNegociacoes.Resultados); ;
            }
            catch(ApiException apiEx)
            {
                if (apiEx.ReasonPhrase == "Not Found"){
                    throw new ($"Sigla da criptomoeda não existe.");
                }

                throw new Exception($"{apiEx.InnerException} \n\r {apiEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.InnerException} \n\r {ex.Message}");
            }
        }
    }
}