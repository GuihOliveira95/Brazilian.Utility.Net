using Brazilian.Utility.Net.Domain.Utility.Integration;
using Brazilian.Utility.Net.Domain.Utility.Queries.GetIPVA;
using Brazilian.Utility.Net.Domain.Utility.Services.Interface;
using System;
using System.Threading.Tasks;

namespace Brazilian.Utility.Net.Domain.Utility.Services
{
    public class UtilityService : IUtilityService
    {
        private readonly IIpvaApi _ipvaApi;

        public UtilityService(IIpvaApi ipvaApi)
        {
            _ipvaApi = ipvaApi;
        }



        public async Task<GetIPVAResponse> GetIPVAAsync(string licensePlate)
        {
            return await _ipvaApi.GetIpvaValues(licensePlate);
        }


    }


}
