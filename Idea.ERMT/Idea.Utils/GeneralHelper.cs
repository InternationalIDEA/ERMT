using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idea.Utils
{
    public static class GeneralHelper
    {
        public static CultureInfo GetDateFormat()
        {
            return new CultureInfo("es-es");
        }
    }
}
