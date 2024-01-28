using Brazilian.Utility.Net.Api.Queries.Vehycle;
using Brazilian.Utility.Net.Domain.Vehycle.Queries.GetIPVA;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Brazilian.Utility.Net.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class VehycleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehycleController(IMediator mediator)
        {
            _mediator = mediator;
        }


        #region GET IPVA

        [HttpGet]
        [ProducesResponseType(typeof(GetIPVAResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> GetIPVAAsync([FromQuery] GetIPVAQuery query)
        {
            var response = await _mediator.Send((GetIPVARequest)query);

            if(response.Found)
                return Ok(response);

            return NoContent();
        }

        #endregion

    }
}
