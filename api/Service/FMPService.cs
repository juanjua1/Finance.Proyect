using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Service
{
    public class FMPService
    {
        private HttpClient _httpCliente;
        private IConfiguration _config
;        public FMPService(HttpClient httpClient, IConfiguration config)
        {
            _httpCliente = httpClient;
            _config = config;
        }
        public async Task<Stock> FindStopckBySymbolAsync(string symbol)
        {
            try {
                var result = await _httpCliente.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apiket={_config["FMPkey]}");
                if(result.IsSuccessStatusCode)
            } catch (Exception e)
            {

            }
        }

    }
}