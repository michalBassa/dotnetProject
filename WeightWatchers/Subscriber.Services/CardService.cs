using AutoMapper;
using Subscriber.Core.InterfaceDAL;
using Subscriber.Core.InterfaceService;
using Subscriber.Core.Response;
using Subscriber.DAL;
using Subscriber.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.Services
{
    public class CardService : ICardService
    {
        readonly ICardRepository _cardRepository;
        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<BaseResponseGeneral<Card>> Login(string password, string email)
        {
            return await _cardRepository.Login(password, email);
        }
        public async Task<BaseResponseGeneral<SubscriptionDetailsResponse>> GetSubscriptionDetails(int id)
        {
            return await _cardRepository.GetSubscriptionDetails(id);
        }

    }
}

