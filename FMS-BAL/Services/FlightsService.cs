using FMS_BAL.IServices;
using FMS_Domain;
using FMS_Domain.Model;
using Microsoft.EntityFrameworkCore;
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

        //public async Task SaveFlight(List<Flight> flights) {
        //    await _db.Flights.AddRangeAsync(flights);
        //    await _db.SaveChangesAsync();
        //}

        public async Task SaveFlight(List<Flight> flights)
        {
            var savedFlights = new List<Flight>();
            foreach (var flight in flights)
            {
                var existingFlight = await _db.Flights.FirstOrDefaultAsync(f => f.FlightCode == flight.FlightCode);

                if (existingFlight != null)
                {                    
                    if (!IsFlightEqual(existingFlight, flight))
                    {                        
                        existingFlight.DepartureAirportCode = flight.DepartureAirportCode;
                        existingFlight.DepartureTime = flight.DepartureTime;
                        existingFlight.ArrivalAirportCode = flight.ArrivalAirportCode;
                        existingFlight.ArrivalTime = flight.ArrivalTime;
                        existingFlight.AirLineCode = flight.AirLineCode;
                        _db.Flights.Update(existingFlight);
                        savedFlights.Add(existingFlight);
                    }
                }
                else
                {                    
                    await _db.Flights.AddAsync(flight);
                    savedFlights.Add(flight);
                }
            }

            await _db.SaveChangesAsync();
        }

        private bool IsFlightEqual(Flight existingFlight, Flight newFlight)
        {            
            return existingFlight.DepartureTime == newFlight.DepartureTime
                   && existingFlight.ArrivalTime == newFlight.ArrivalTime;
        }
    }
}
