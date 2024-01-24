using Brazilian.Utility.Net.Domain.Utility.Queries.GetIPVA;
using System.Threading.Tasks;

namespace Brazilian.Utility.Net.Domain.Utility.Services.Interface
{
    public interface IUtilityService
    {
        Task<GetIPVAResponse> GetIPVAAsync(string licensePlate);

    }
}
