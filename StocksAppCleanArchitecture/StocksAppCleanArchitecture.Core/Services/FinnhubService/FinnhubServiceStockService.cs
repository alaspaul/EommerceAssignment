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
    public class FinnhubServiceStockService : IFinnhubServiceStockService
    {
        private readonly IFinnhubRepository _finnhubRepository;


        public FinnhubServiceStockService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }



        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            List<Dictionary<string, string>>? responseDictionary = await _finnhubRepository.GetStocks();

            return responseDictionary;
        }


    }
}

