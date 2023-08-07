using AutoMapper;
using Domain.Models;
using MercadoBitcoinAdapter.Clients;

namespace Adapter.MercadoBitcoinAdapter
{
    public class MercadoBitcoinMapperProfile : Profile
    {
        public MercadoBitcoinMapperProfile()
        {
            CreateMap<ResultadosNegociacoesDoDiaGetResult, NegociacoesDoDia>()
                .ForMember(destino => destino.DataHora,
                    opt => opt.MapFrom(origem =>
                    new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).
                    AddSeconds(origem.DataHora).ToLocalTime()));
        }
    }
}
