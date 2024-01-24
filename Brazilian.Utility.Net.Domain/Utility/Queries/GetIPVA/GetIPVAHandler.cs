

using Brazilian.Utility.Net.Domain.Utility.Services.Interface;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Brazilian.Utility.Net.Domain.Utility.Queries.GetIPVA
{
    public class GetIPVAHandler : IRequestHandler<GetIPVARequest, GetIPVAResponse>
    {
        private readonly IUtilityService _utilityService;

        public GetIPVAHandler(IUtilityService utilityService)
        {
            _utilityService = utilityService;
        }

        public async Task<GetIPVAResponse> Handle(GetIPVARequest request, CancellationToken cancellationToken)
        {
          
            var GetIPVA = await _utilityService.GetIPVAAsync(request.LicensePlate);

            return GetIPVA;

        }

    }
}
