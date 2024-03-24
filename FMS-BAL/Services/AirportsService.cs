using FMS_BAL.IServices;
using FMS_Domain;
using FMS_Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS_BAL.Services
{
    public class AirportsService : IAirportsService
    {
        private readonly FlightsManagementSystemDbContext _db;

        public AirportsService(FlightsManagementSystemDbContext db)
        {
            _db = db;
        }
        public async Task SaveAirport(List<Airport> airports)
        {
            var savedAirports = new List<Airport>();
            foreach (var airport in airports)
            {
                var existingAirport = await _db.Airports.FirstOrDefaultAsync(a => a.Code == airport.Code);

                if (existingAirport != null)
                {
                    if (!IsAirportEqual(existingAirport, airport))
                    {
                        existingAirport.Code = airport.Code;
                        existingAirport.Name = airport.Name;
                        existingAirport.CountryName= airport.CountryName;
                        existingAirport.CountryCode = airport.CountryCode;
                        existingAirport.CityCode= airport.CityCode;
                        existingAirport.Longitude= airport.Longitude;
                        existingAirport.Latitude= airport.Latitude;
                        _db.Airports.Update(existingAirport);
                        savedAirports.Add(existingAirport);
                    }
                }
                else
                {
                    await _db.Airports.AddAsync(airport);
                    savedAirports.Add(airport);
                }
            }

            await _db.SaveChangesAsync();
        }

        private bool IsAirportEqual(Airport existingAirport, Airport newAirport)
        {
            return existingAirport.Name == newAirport.Name
                   && existingAirport.Longitude == newAirport.Longitude
                   && existingAirport.Latitude == newAirport.Latitude;
        }
    }
}
