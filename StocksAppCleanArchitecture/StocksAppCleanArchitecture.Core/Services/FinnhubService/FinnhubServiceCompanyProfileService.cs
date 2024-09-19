using Microsoft.Extensions.Configuration;
using RepositoryContracts;
using ServiceContracts.FinnhubServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StocksAppCleanArchitecture.Core.Services.FinnhubService
{
    public class FinnhubServiceCompanyProfileService : IFinnhubServiceCompanyProfileService
    {
        private readonly IFinnhubRepository _finnhubRepository;


        public FinnhubServiceCompanyProfileService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }


        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            Dictionary<string, object>? responseDictionary = await _finnhubRepository.GetCompanyProfile(stockSymbol);

            return responseDictionary;
        }

    }
}

