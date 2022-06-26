using solarLab.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Domain.Entities
{
    public class ChatEntity:BaseEntity
    {
        public ICollection<MessageEntity> Messages { get; set; }

        public ICollection<ApplicationUser> Members { get; set; }
    }
}
