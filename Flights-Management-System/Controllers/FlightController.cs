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


        public FlightController(
            ILogger<FlightController> logger,
            IConfiguration configuration,
            HttpClient httpClient)
        {
            _logger = logger;
            _configuration = configuration;
            _aviationStackApiKey = _configuration.GetSection("API-KEYS:Aviationstack").Value!.ToString()!;
            _aviationStackAccessKey = _configuration.GetSection("ACCESS-KEYS:Aviationstack").Value!.ToString()!;
            _httpClient = httpClient;
        }

        [HttpPost]
        [Route("SearchFlights")]
        public async Task<ActionResult<RootObject>> SearchFlights()
        {
            string apiUrl = $"{_aviationStackApiKey}/flights?access_key={_aviationStackAccessKey}&limit=10";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(jsonResponse)!;

                    return Ok(rootObject);
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
