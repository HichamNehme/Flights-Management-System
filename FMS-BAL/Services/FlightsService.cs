using FMS_BAL.IServices;
using FMS_Domain;
using FMS_Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace FMS_BAL.Services
{
    public class FlightsService : IFlightsService
    {

        private readonly FlightsManagementSystemDbContext _db;

        public FlightsService(FlightsManagementSystemDbContext db) { 
            _db = db;
        }

        public async Task SaveFlight(List<Flight> flights) {
            await _db.Flights.AddRangeAsync(flights);
            await _db.SaveChangesAsync();
        }

    }
}
