using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;

namespace API_Licenta.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public class SensorData
        {
            public int SensorId { get; set; }
            public string SensorType { get; set; }
            public double Value { get; set; }
        }


        [HttpGet("ledPinHall")]
        public async Task<ActionResult<bool>> GetSensorData()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://192.168.251.175/ledPinHall");
            var responseContent = await response.Content.ReadAsStringAsync();
            bool isHallPinOn = bool.Parse(responseContent);

            // Code to retrieve sensor data goes here
            // Return the sensor data as a JSON object
            return Ok(isHallPinOn);
        }

        [HttpPost("ledPinHall")]
        public async Task<ActionResult> CreateSensorData([FromBody] SensorData data)
        {
            // Read the existing data from the JSON file
            var jsonData = await System.IO.File.ReadAllTextAsync(@"C:\Users\nicolae.bilac\Desktop\API Licenta\api\Values\hallLightsData.json");
            var hallLightsData = JsonConvert.DeserializeObject<List<SensorData>>(jsonData);

            // Add the new data to the list
            hallLightsData.Add(data);

            // Serialize the updated list to JSON and write it back to the file
            var updatedJsonData = JsonConvert.SerializeObject(hallLightsData);
            await System.IO.File.WriteAllTextAsync("api/Values/hallLightsData.json", updatedJsonData);

            // Return an HTTP 200 OK response
            return Ok();
        }

    }
}
