using FMS_BAL.IServices;
using FMS_BAL.Services;
using FMS_Domain.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static FMS_BAL.ApiWrapper.AviationStackResponse;

namespace Flights_Management_System.Controllers
{
    public class AirportController : Controller
    {
        private readonly ILogger<AirportController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _aviationStackApiKey;
        private readonly string _aviationStackAccessKey;
        private readonly HttpClient _httpClient;
        private readonly IAirportsService _airportsService;

        public AirportController(ILogger<AirportController> logger,
            IConfiguration configuration,
            HttpClient httpClient,
            IAirportsService airportsService)
        {
            _logger = logger;
            _configuration = configuration;
            _aviationStackApiKey = _configuration.GetSection("API-KEYS:Aviationstack").Value!.ToString()!;
            _aviationStackAccessKey = _configuration.GetSection("ACCESS-KEYS:Aviationstack").Value!.ToString()!;
            _httpClient = httpClient;
            _airportsService = airportsService;
        }

        [HttpPost]
        [Route("SearchAirports")]
        public async Task<ActionResult<AirportRoot>> SearchAirports([FromBody] SearchAirportDTO searchAirportDTO)
        {
            try
            {
                var Offset = (searchAirportDTO.PageNumber - 1) * searchAirportDTO.Limit;
                string apiUrl = $"{_aviationStackApiKey}/airports?access_key={_aviationStackAccessKey}&limit={searchAirportDTO.Limit}&offset={Offset}";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    AirportRoot rootObject = JsonConvert.DeserializeObject<AirportRoot>(jsonResponse)!;

                    var extractedData = rootObject.data.Select(x => new FMS_Domain.Model.Airport
                    {
                        Code = x.iata_code,
                        Name = x.airport_name,
                        CountryCode = x.country_iso2,
                        CountryName = x.country_name,
                        CityCode = x.city_iata_code,
                        Longitude = x.longitude,
                        Latitude = x.latitude
                    }
                    ).ToList();

                    await _airportsService.SaveAirport(extractedData);

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
