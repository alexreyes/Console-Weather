using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonApiClient
{
    class ConverTimeTo
    {
        public string FromUnixTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();

            string timeString = dtDateTime + "";
            string finalTimeString = timeString.Remove(0, timeString.IndexOf("2017") + 4);

            return finalTimeString;
        }
    }
}
