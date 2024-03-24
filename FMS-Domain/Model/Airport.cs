using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS_Domain.Model
{
    public class Airport
    {
        [Key]
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? CountryCode { get; set; }
        public string? CityCode { get; set; }
        public string? CountryName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
