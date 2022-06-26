using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveEntityFromString(this string source)
        {
            return source.Replace("Entity", "");
        }
    }
}
