using Brazilian.Utility.Net.Domain.Vehycle.Queries.GetIPVA;
using System.Threading.Tasks;

namespace Brazilian.Utility.Net.Domain.Vehycle.Services.Interface
{
    public interface IVehycleService
    {
        Task<GetIPVAResponse> GetIPVAAsync(string licensePlate);

    }
}
