using FMS_Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS_BAL.IServices
{
    public interface IAirportsService
    {
        Task SaveAirport(List<Airport> airports);
    }
}
