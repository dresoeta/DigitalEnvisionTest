using System;
using System.IO;
using System.Net;
using AutoMapper;
using DigitalEnvision.Models;

namespace DigitalEnvision.Helpers
{
    public static class Extentions
    {
        public static string HitNotAllowed = "NotAllowed";
        public static string HitNotFound = "NotFound";
        public static string HitUnexpected = "Unexpected";
       


        public static TDestination Map<TSource1, TSource2, TDestination>(
            this IMapper mapper, TSource1 source1, TSource2 source2)
        {
            var destination = mapper.Map<TSource1, TDestination>(source1);
            return mapper.Map(source2, destination);
        }

        public static Result CheckLocation(string location)
        {
            try
            {
                var result = string.Empty;
                var GetTimezoneList = (HttpWebRequest) WebRequest.Create("http://worldtimeapi.org/api/timezone");
                GetTimezoneList.ContentType = "application/json";
                GetTimezoneList.Method = "GET";

                var TimezoneList = (HttpWebResponse) GetTimezoneList.GetResponse();
                using (var streamReader = new StreamReader(TimezoneList.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();

                }


                var timezone = result;

                if (timezone.Contains(location))
                    return new Result {success = true, message = "Location Validated"};

                return new Result {success = false, message = "Location Not Found"};
            }
            catch (Exception ex)
            {
                return new Result {success = false, message = "Something Went Wrong"};
            }
           
        }

        public static string ToString(this DateTime? dt, string format)
            => dt == null ? "n/a" : ((DateTime)dt).ToString(format);



    }
}
