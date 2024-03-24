using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS_Domain.Model
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }

        public string? DepartureAirportCode { get; set; }

        public DateTime DepartureTime { get; set; }

        public string? ArrivalAirportCode { get; set; }

        public DateTime ArrivalTime { get; set; }

        public string? AirLineCode { get; set; }

        public string? FlightCode { get; set; }
    }
}
