using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FMS_BAL.ApiWrapper
{
    public class AviationStackResponse
    {
        #region Flight
        public class Pagination
        {
            public int limit { get; set; }
            public int offset { get; set; }
            public int count { get; set; }
            public int total { get; set; }
        }

        public class Departure
        {
            public string airport { get; set; }
            public string timezone { get; set; }
            public string iata { get; set; }
            public string icao { get; set; }
            public object terminal { get; set; }
            public object gate { get; set; }
            public object delay { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime estimated { get; set; }
            public object actual { get; set; }
            public object estimated_runway { get; set; }
            public object actual_runway { get; set; }
        }
        public class Arrival
        {
            public string airport { get; set; }
            public string timezone { get; set; }
            public string iata { get; set; }
            public string icao { get; set; }
            public string terminal { get; set; }
            public object gate { get; set; }
            public object baggage { get; set; }
            public object delay { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime estimated { get; set; }
            public object actual { get; set; }
            public object estimated_runway { get; set; }
            public object actual_runway { get; set; }
        }

        public class Airline
        {
            public string name { get; set; }
            public string iata { get; set; }
            public string icao { get; set; }
        }

        public class Flight
        {
            public string number { get; set; }
            public string iata { get; set; }
            public string icao { get; set; }
        }

        public class Datum
        {
            public string flight_date { get; set; }
            public string flight_status { get; set; }
            public Departure departure { get; set; }
            public Arrival arrival { get; set; }
            public Airline airline { get; set; }
            public Flight flight { get; set; }
            public object aircraft { get; set; }
            public object live { get; set; }
        }

        public class RootObject
        {
            public Pagination pagination { get; set; }
            public List<Datum> data { get; set; }
        }
        #endregion

        #region Airport        
        public class AirportPagination
        {
            public int offset { get; set; }
            public int limit { get; set; }
            public int count { get; set; }
            public int total { get; set; }
        }

        public class AirportData
        {
            public string id { get; set; }
            public string? gmt { get; set; }
            public string? airport_id { get; set; }
            public string? iata_code { get; set; }
            public string? city_iata_code { get; set; }
            public string? icao_code { get; set; }
            public string? country_iso2 { get; set; }
            public string? geoname_id { get; set; }
            public string? latitude { get; set; }
            public string? longitude { get; set; }
            public string? airport_name { get; set; }
            public string? country_name { get; set; }
            public object phone_number { get; set; }
            public string? timezone { get; set; }
        }

        public class AirportRoot
        {
            [JsonPropertyName("Pagination")]
            public AirportPagination pagination { get; set; }
            [JsonPropertyName("Datum")]
            public List<AirportData> data { get; set; }
        }
        #endregion
    }
}
