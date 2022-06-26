using solarLab.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.Search
{
    public class FilterDto
    {
        public string SearchString { get; set; }

        public SearchIn SearchIn { get; set; }

        public decimal? SalaryFrom { get; set; }

        public decimal? SalaryTo { get; set; }

        public List<Experience> WorkExperience { get; set; }

        public List<Schedule> WorkSchedule { get; set; }

        public Sorting SortBy { get; set; }

    }
}
