using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb
{
    public static class SD
    {
        public static string ApiBaseUrl = "https://localhost:44340/";
        public static string NationalParkAPIPath = ApiBaseUrl + "api/v1/nationalparks/";
        public static string TrailAPIPath = ApiBaseUrl + "api/v1/trails/";
        public static string AccountAPIPath = ApiBaseUrl + "api/v1/Users/";
    }
}
