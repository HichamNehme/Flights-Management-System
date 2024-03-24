using FMS_BAL.IServices;
using FMS_Domain.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using static FMS_BAL.ApiWrapper.AviationStackResponse;
using static System.Net.WebRequestMethods;

namespace Flights_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly ILogger<FlightController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _aviationStackApiKey;
        private readonly string _aviationStackAccessKey;
        private readonly HttpClient _httpClient;
        private readonly IFlightsService _flightService;

        public FlightController(
            ILogger<FlightController> logger,
            IConfiguration configuration,
            HttpClient httpClient,
            IFlightsService flightService)
        {
            _logger = logger;
            _configuration = configuration;
            _aviationStackApiKey = _configuration.GetSection("API-KEYS:Aviationstack").Value!.ToString()!;
            _aviationStackAccessKey = _configuration.GetSection("ACCESS-KEYS:Aviationstack").Value!.ToString()!;
            _httpClient = httpClient;
            _flightService = flightService;
        }

        [HttpPost]
        [Route("SearchFlights")]
        public async Task<ActionResult<RootObject>> SearchFlights([FromBody]SearchFlightDTO searchFlightDTO)
        {
            try
            {
                var Offset = (searchFlightDTO.PageNumber - 1) * searchFlightDTO.Limit;
                string apiUrl = $"{_aviationStackApiKey}/flights?access_key={_aviationStackAccessKey}&limit={searchFlightDTO.Limit}&offset={Offset}";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(jsonResponse)!;

                    var extractedData = rootObject.data.Select(x => new FMS_Domain.Model.Flight
                    {
                        DepartureAirportCode = x.departure.iata,
                        DepartureTime = x.departure.scheduled,
                        ArrivalAirportCode = x.arrival.iata,
                        ArrivalTime = x.arrival.scheduled,
                        AirLineCode = x.airline.iata,
                        FlightCode = x.flight.iata,
                    }
                        ).ToList();

                    await _flightService.SaveFlight(extractedData);

                    return Ok(extractedData);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, $"Failed to fetch data. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error fetching data: {ex.Message}");
            }
        }

    }
}
