using FMS_Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS_Domain
{

    public class FlightsManagementSystemDbContext : DbContext
    {
        public FlightsManagementSystemDbContext(DbContextOptions<FlightsManagementSystemDbContext> options) : base(options)
        {

        }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Airport> Airports { get; set; }
    }
}
