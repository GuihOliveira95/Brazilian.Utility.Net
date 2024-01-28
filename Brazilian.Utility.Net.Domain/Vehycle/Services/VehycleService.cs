using Brazilian.Utility.Net.Domain.Vehycle.Integration;
using Brazilian.Utility.Net.Domain.Vehycle.Queries.GetIPVA;
using Brazilian.Utility.Net.Domain.Vehycle.Services.Interface;
using System;
using System.Threading.Tasks;

namespace Brazilian.Utility.Net.Domain.Vehycle.Services
{
    public class VehycleService : IVehycleService
    {
        private readonly IIpvaApi _ipvaApi;

        public VehycleService(IIpvaApi ipvaApi)
        {
            _ipvaApi = ipvaApi;
        }



        public async Task<GetIPVAResponse> GetIPVAAsync(string licensePlate)
        {
            return await _ipvaApi.GetIpvaValues(licensePlate);
        }


    }


}
