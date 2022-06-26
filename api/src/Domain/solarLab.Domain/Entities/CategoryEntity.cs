using solarLab.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Domain.Entities
{
    public class CategoryEntity : BaseEntity
    {
        public string Title { get; set; }

        public ICollection<PostEntity> Posts { get; set; }
    }
}
