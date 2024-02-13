using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Subscriber.Core;
using Subscriber.Core.DTO;
using Subscriber.Core.InterfaceDAL;
using Subscriber.Core.Model;
using Subscriber.Core.Response;
using Subscriber.Data;
using Subscriber.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.DAL
{
    public class SubscriberRepository : ISubscriberRepository
    {
        WeightWatchersContext _weightWatchersContext;
        readonly IMapper _mapper;
        public SubscriberRepository(WeightWatchersContext weightWatchersContext, IMapper mapper)
        {
            _weightWatchersContext = weightWatchersContext;
            _mapper = mapper;
        }

        public async Task<BaseResponseGeneral<bool>> ToSubscriber(SubscriberModle subscriberModle, double height)
        {
            try
            {
                BaseResponseGeneral<bool> response = new BaseResponseGeneral<bool>();

                var createdSubscriber = await _weightWatchersContext.SubscribersDb.AddAsync(_mapper.Map<Subscribers>(subscriberModle));

                await _weightWatchersContext.SaveChangesAsync();
                Card defaultCard = new Card
                {
                    SubscriberId = createdSubscriber.Entity.Id,
                    OpenDate = DateTime.Now,
                    UpDate = DateTime.Now,
                    BMI = 0,
                    Height = height
                };
                _weightWatchersContext.cardsDb.Add(defaultCard);
                await _weightWatchersContext.SaveChangesAsync();
                response.Response = true;
                response.Succssed = true;
                response.Massage = " added successfuly";


                return response;
            }
            catch (Exception ex)
            {
                throw new MyException(205, "ERROR", ex.Message);
            }

        }
        public async Task<bool> SubscriberEmailExists(string email)
        {
            return await _weightWatchersContext.SubscribersDb.AnyAsync(s => s.Email == email);
        }

    }
}
