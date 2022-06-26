using solarLab.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Domain.Entities
{
    public class MessageEntity:BaseEntity
    {
        public Guid SenderId { get; set; }
        public string Body { get; set; }
        public Guid ChatId { get; set; }

        public ApplicationUser Sender { get; set; }

        public ChatEntity Chat { get; set; }
    }
}
