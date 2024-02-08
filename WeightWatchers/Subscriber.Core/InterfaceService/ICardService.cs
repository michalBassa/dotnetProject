using Subscriber.Core.Response;
using Subscriber.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.Core.InterfaceService
{
    public interface ICardService
    {
        Task<BaseResponseGeneral<Card>> Login(string password, string email);
        Task<BaseResponseGeneral<SubscriptionDetailsResponse>> GetSubscriptionDetails(int id);

    }
}

