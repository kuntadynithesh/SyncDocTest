using Microsoft.AspNetCore.Mvc;
using Syncfusion.EJ2.DocumentEditor;

namespace SyncDocTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }    

        [HttpPost]
        public FileStreamResult Export[FromBody] SaveParameter data)
        {
            // Convert the content to a Word document stream
            Stream document = WordDocument.Save(data.content, FormatType.Docx);
        
            // Reset the position of the stream to ensure proper reading
            document.Position = 0;
        
            // Create a FileStreamResult to return the file stream
            var fileStreamResult = new FileStreamResult(document, "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                FileDownloadName = "sample.docx"
            };
        
            return fileStreamResult;
        }

        public class SaveParameter
        {
            public string content { get; set; }
        }

    }
}
