using Subscriber.Core.InterfaceDAL;
using Subscriber.Core.InterfaceService;
using Subscriber.Core.Model;
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
    public class SubscriberService : ISubscriberService
    {
        readonly ISubscriberRepository _subscriberRepository;
        public SubscriberService(ISubscriberRepository subscriberRepository)
        {
            _subscriberRepository = subscriberRepository;
        }
        
        public async Task<BaseResponseGeneral<bool>> ToSubscriber(SubscriberModle subscriberModle, double height)
        {
            BaseResponseGeneral<bool> response = new BaseResponseGeneral<bool>();
            if (!await _subscriberRepository.SubscriberEmailExists(subscriberModle.Email))
            {
                response.Succssed = false;
                response.Massage = "Email already exists";
            }

            response = await _subscriberRepository.ToSubscriber(subscriberModle, height);

            return response;
        }


    }
}

