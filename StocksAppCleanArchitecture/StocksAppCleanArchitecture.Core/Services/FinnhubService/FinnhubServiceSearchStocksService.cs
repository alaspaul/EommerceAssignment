using Microsoft.Extensions.Configuration;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.FinnhubServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StocksAppCleanArchitecture.Core.Services.FinnhubService
{
    public class FinnhubServiceSearchStocksService : IFinnhubServiceSearchStocksService
    {
        private readonly IFinnhubRepository _finnhubRepository;


        public FinnhubServiceSearchStocksService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }


        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            Dictionary<string, object>? responseDictionary = await _finnhubRepository.SearchStocks(stockSymbolToSearch);

            return responseDictionary;
        }
    }
}

