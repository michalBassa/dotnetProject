using AutoMapper;
using Subscriber.Core;
using Subscriber.Core.InterfaceDAL;
using Subscriber.Core.InterfaceService;
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
    public class CardRepository : ICardRepository
    {
        WeightWatchersContext _weightWatchersContext;

        public CardRepository(WeightWatchersContext weightWatchersContext)
        {
            _weightWatchersContext = weightWatchersContext;
            
        }

        //public async Task<BaseResponseGeneral<Card>> Login(string password, string email)
        //{
        //    try
        //    {
        //        Subscribers subscriber = _weightWatchersContext.SubscribersDb.Where(s => s.Password == password && s.Email == email).FirstOrDefault();
        //        BaseResponseGeneral<Card> response = new BaseResponseGeneral<Card>();
        //        if (subscriber != null)
        //        {
        //            response.Response = _weightWatchersContext.cardsDb.Where(c => c.Id == subscriber.Id).First();
        //            response.Succssed = true;
        //            response.Massage = "Succssed";
        //            return response;
        //        }
        //        response.Succssed = false;
        //        response.Massage = "email and password didnot good";
        //        response.Response = null;
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new MyException(401, "Error code", ex.Message);
        //    }
        //}
        public async Task<BaseResponseGeneral<LoginResponse>> Login(string password, string email)
        {
            try
            {
                Subscribers subscriber = _weightWatchersContext.SubscribersDb.Where(s => s.Password == password && s.Email == email).FirstOrDefault();
                BaseResponseGeneral<LoginResponse> response = new BaseResponseGeneral<LoginResponse>();
                response.Response = new LoginResponse();
                if (subscriber != null)
                {
                    
                    response.Response.Id = _weightWatchersContext.cardsDb.Where(c => c.SubscriberId == subscriber.Id).First().Id;
                    response.Succssed = true;
                    response.Massage = "Succssed";
                    return response;
                }
                response.Succssed = false;
                response.Massage = "email and password didnot good";
                response.Response.Id = -1;
                return response;
            }
            catch (Exception ex)
            {
                throw new MyException(401, "Error code", ex.Message);
            }
        }

        public async Task<BaseResponseGeneral<SubscriptionDetailsResponse>> GetSubscriptionDetails(int id)
        {

            try
            {
                BaseResponseGeneral<SubscriptionDetailsResponse> response = new BaseResponseGeneral<SubscriptionDetailsResponse>();

                Card card = _weightWatchersContext.cardsDb.Where(c => c.Id == id).FirstOrDefault();
                if (card != null)
                {
                    Subscribers subscriber = _weightWatchersContext.SubscribersDb.Where(s => s.Id == id).FirstOrDefault();
                    if (subscriber != null)
                    {
                        response.Succssed = true;
                        response.Massage = "succed";
                        response.Response = new SubscriptionDetailsResponse();
                        response.Response.Id = card.Id;
                        response.Response.Weight = card.Weight;
                        response.Response.Height = card.Height;
                        response.Response.BMI = card.BMI;
                        response.Response.FirstName = subscriber.FirstName;
                        response.Response.LastName = subscriber.LastName;
                        return response;
                    }
                }
                response.Succssed = false;
                response.Massage = "failed";
                return response;
            }
            catch (Exception ex)
            {
                throw new MyException(400, "Error: not exsits", ex.Message);
            }
        }

    }
}
