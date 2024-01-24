using Brazilian.Utility.Net.Domain.Utility.Entities;

namespace Brazilian.Utility.Net.Domain.Utility.Queries.GetIPVA
{
    public class GetIPVAResponse
    {

        public VehycleUtility Data { get; set; }

        public bool Found { get; set; }

        public static GetIPVAResponse Founded(VehycleUtility vehycleUtility) => new()
        {
            Data = vehycleUtility,
            Found = true
        };

        public static GetIPVAResponse NotFound() => new( )
        {

            Found = false
        };

        public static implicit operator GetIPVAResponse(VehycleUtility vehycleUtility)
        {

            if(vehycleUtility.Ipva == null)
                return NotFound();

            return Founded(vehycleUtility);
        }
    }
}
