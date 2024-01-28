using MediatR;


namespace Brazilian.Utility.Net.Domain.Vehycle.Queries.GetIPVA
{
    public class GetIPVARequest : IRequest<GetIPVAResponse>
    {
        public string LicensePlate { get; set; }
    }
}
