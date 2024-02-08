using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subscriber.Core.DTO;
using Subscriber.Core.InterfaceService;
using Subscriber.Core.Model;
using Subscriber.Core.Response;
using Subscriber.Services;

namespace Subscriber.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        readonly ISubscriberService _subscriberService;
        readonly IMapper _mapper;

        public SubscriberController(ISubscriberService subscriberService, IMapper mapper)
        {
            _subscriberService = subscriberService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<BaseResponseGeneral<bool>>> ToSubscriber([FromBody] SubscriberDTO subscriberDTO)
        {
            var response = await _subscriberService.ToSubscriber(_mapper.Map<SubscriberModle>(subscriberDTO), subscriberDTO.Height);
            if (response.Succssed == false)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
