using Subscriber.Core.Model;
using Subscriber.Core.Response;
using Subscriber.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.Core.InterfaceService
{
    public interface ISubscriberService
    {
        Task<BaseResponseGeneral<bool>> ToSubscriber(SubscriberModle subscriberModle, double height);

    }
}

