
using System.Threading.Tasks;
using Brazilian.Utility.Net.Domain.Vehycle.Entities;

namespace Brazilian.Utility.Net.Domain.Vehycle.Integration
{
	public interface IIpvaApi
	{
        Task<VehycleUtility> GetIpvaValues(string licensePlate);
    }
}

