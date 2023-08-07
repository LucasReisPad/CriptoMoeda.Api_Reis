using DataAcess.Data;
using Domain.Adapters;
using Domain.Models;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace Application
{
    public class CriptoMoedaService : ICriptoMoedaService
    {
        private readonly IMercadoBitcoinAdapter mercadoBitcoinAdapter;
        private readonly AppDbContext _db;
        
        public CriptoMoedaService(IMercadoBitcoinAdapter mercadoBitcoinAdapter, AppDbContext db)
        {
            this.mercadoBitcoinAdapter = mercadoBitcoinAdapter ??
                throw new ArgumentNullException(nameof(mercadoBitcoinAdapter));
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task UpsertCoinDataAsync(CoinData coinData)
        {
            var coinFromDb = _db.CoinDatas.AsNoTracking().FirstOrDefault(x => x.Coin == coinData.Coin);
            var registerCoin = new CoinDataRegisterSearch()
            {
                Coin = coinData.Coin,
                MaiorPreco = coinData.MaiorPreco,
                MenorPreco = coinData.MenorPreco,
                QuantidadeNegociada = coinData.QuantidadeNegociada,
                PrecoUnitario = coinData.PrecoUnitario,
                MaiorPrecoOfertado = coinData.MaiorPrecoOfertado,
                MenorPrecoOfertado = coinData.MenorPrecoOfertado,
                DataHora = coinData.DataHora

            };

            if (coinFromDb == null)
            {
                await _db.CoinDatas.AddAsync(coinData);
                await _db.RegisterSearches.AddAsync(registerCoin);
                await _db.SaveChangesAsync();
            }
            else
            {
                coinData.Id = coinFromDb.Id;
                await _db.RegisterSearches.AddAsync(registerCoin);
                _db.CoinDatas.Update(coinData);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<NegociacoesDoDia> ObterDadosNegociacoesDoDiaAsync(string siglaMoeda)
        {
            if (string.IsNullOrWhiteSpace(siglaMoeda))
            {
                throw new Exception("Sigla escolhida não é valida.");
            }

            return await mercadoBitcoinAdapter.ObterDadosNegociacoesDoDiaAsync(siglaMoeda);
        }

        
        public async Task<IEnumerable<CoinDataRegisterSearch>> GetCoinDataRegisterSearchAsync(string siglaMoeda)
        {
            IQueryable<CoinDataRegisterSearch> query = _db.RegisterSearches;
            query = query.Where(x => x.Coin == siglaMoeda);
            return await query.ToListAsync();
        }

        public async Task<CoinData> GetCoinDataAtt(string siglaMoeda)
        {
            var obj = await _db.CoinDatas.FirstOrDefaultAsync(x=> x.Coin == siglaMoeda);
            return obj;
        }
    }
}