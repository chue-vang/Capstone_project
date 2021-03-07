using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HersFlowers.APIKey
{
    public static class APIKey
    {
        private static string googleMapKey = "";
        public static string GoogleMapKey
        {
            get => googleMapKey;
        }
    }
}
