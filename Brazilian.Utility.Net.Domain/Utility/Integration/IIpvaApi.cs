
using System.Threading.Tasks;
using Brazilian.Utility.Net.Domain.Utility.Entities;

namespace Brazilian.Utility.Net.Domain.Utility.Integration
{
	public interface IIpvaApi
	{
        Task<VehycleUtility> GetIpvaValues(string licensePlate);
    }
}

