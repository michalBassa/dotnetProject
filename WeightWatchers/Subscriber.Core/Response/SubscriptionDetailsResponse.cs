using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.Core.Response
{
    public class SubscriptionDetailsResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int BMI { get; set; } = 0;

        public double Height { get; set; }

        public double Weight { get; set; }

    }
}
