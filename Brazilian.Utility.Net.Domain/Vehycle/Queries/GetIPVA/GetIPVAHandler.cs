

using Brazilian.Utility.Net.Domain.Vehycle.Services.Interface;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Brazilian.Utility.Net.Domain.Vehycle.Queries.GetIPVA
{
    public class GetIPVAHandler : IRequestHandler<GetIPVARequest, GetIPVAResponse>
    {
        private readonly IVehycleService _vehycleService;

        public GetIPVAHandler(IVehycleService vehycleService)
        {
            _vehycleService = vehycleService;
        }

        public async Task<GetIPVAResponse> Handle(GetIPVARequest request, CancellationToken cancellationToken)
        {
          
            var GetIPVA = await _vehycleService.GetIPVAAsync(request.LicensePlate);

            return GetIPVA;

        }

    }
}
