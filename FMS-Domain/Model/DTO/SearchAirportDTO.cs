using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS_Domain.Model.DTO
{
    public class SearchAirportDTO
    {
        public int PageNumber { get; set; }
        public int Limit { get; set; }
    }
}
