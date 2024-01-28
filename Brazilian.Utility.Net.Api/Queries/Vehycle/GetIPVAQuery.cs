using Brazilian.Utility.Net.Domain.Vehycle.Queries.GetIPVA;
using Microsoft.AspNetCore.Mvc;

namespace Brazilian.Utility.Net.Api.Queries.Vehycle
{
    [BindProperties(SupportsGet = true)]
    public class GetIPVAQuery
    {

        [BindProperty(Name = "LicensePlate", SupportsGet = true)]
        public string LicensePlate { get; set; }

        public static implicit operator GetIPVARequest(GetIPVAQuery query)
        {
            if (query is null)
            {
                return null;
            }

            return new()
            {
                LicensePlate = query.LicensePlate,
            };
        }
    }
}
