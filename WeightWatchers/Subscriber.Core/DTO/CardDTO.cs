using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.Core.DTO
{
    public class CardDTO
    {
        public DateTime UpDate { get; set; }

        public int BMI { get; set; } = 0;

        public double Height { get; set; }

        public double Weight { get; set; }
    }
}

