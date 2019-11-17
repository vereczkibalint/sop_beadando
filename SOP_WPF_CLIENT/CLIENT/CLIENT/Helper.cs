using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIENT
{
    public static class Helper
    {
        public static bool IsAnyEmptyOrNull(params string[] values)
        {
            return values.Any(x => x == "" || x == null);
        }
    }
}
