using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Final_Project.Utils
{


    public class CountryCodes
    {
        public static string[] Codes = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
            .Select(c => new RegionInfo(c.LCID).TwoLetterISORegionName)
            .Distinct()
            .OrderBy(s => s)
            .ToArray();
    }

}
