using MediatR;


namespace Brazilian.Utility.Net.Domain.Utility.Queries.GetIPVA
{
    public class GetIPVARequest : IRequest<GetIPVAResponse>
    {
        public string LicensePlate { get; set; }
    }
}
