using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.Data.Entities
{
    [Table("card")]
    public class Card
    {

        [Key]
        [Required]
        public int Id { get; set; }

        public int? SubscriberId { get; set; }

        [ForeignKey(nameof(SubscriberId))]
        public Subscribers SubscriberCard { get; set; }

        public DateTime OpenDate { get; set; }

        public DateTime UpDate { get; set; }

        public int BMI { get; set; } = 0;

        public double Height { get; set; }

        public double Weight { get; set; }
    }
}
