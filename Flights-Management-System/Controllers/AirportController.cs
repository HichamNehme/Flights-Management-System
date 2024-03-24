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

        public AirportController(ILogger<AirportController> logger,
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
        [Route("SearchAirports")]
        public async Task<ActionResult<AirportRoot>> SearchAirports()
        {
            string apiUrl = $"{_aviationStackApiKey}/airports?access_key={_aviationStackAccessKey}&limit=10";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    AirportRoot rootObject = JsonConvert.DeserializeObject<AirportRoot>(jsonResponse)!;

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
