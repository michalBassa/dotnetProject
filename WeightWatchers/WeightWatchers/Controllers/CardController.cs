using AutoMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subscriber.Core.DTO;
using Subscriber.Core.InterfaceService;
using Subscriber.Core.Response;
using Subscriber.Data.Entities;
using Subscriber.Services;

namespace Subscriber.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        readonly ICardService _cardService;
        readonly IMapper _mapper;

        public CardController(ICardService cardService, IMapper mapper)
        {
            _cardService = cardService;
            _mapper = mapper;
        }

        [HttpPost("/Login")]
        public async Task<ActionResult<BaseResponseGeneral<CardDTO>>> Login([FromBody] LoginDTO loginDTO)
        {
            
            var card = await _cardService.Login(loginDTO.Password, loginDTO.Email);
            if (card == null)
                return BadRequest(card);
            return Ok(_mapper.Map<CardDTO>(card));
        }

        [HttpGet("{id}")]
        
        public async Task<ActionResult<BaseResponseGeneral<SubscriptionDetailsResponse>>> GetSubscriptionDetails([FromRoute] int id)
        {

            var response = await _cardService.GetSubscriptionDetails(id);
            if (response == null)
                return NotFound();
            return Ok(response);
        }
    }
}