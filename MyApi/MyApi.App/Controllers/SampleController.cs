using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace MyApi.App.Controllers{
    [Route("api/[Controller]")]
    [ApiController]
    public class SampleController: ControllerBase{
        //fields
        private readonly ILogger<SampleController> logger;
        private static readonly List<int> samples = new() {12};
        public SampleController(ILogger<SampleController> logger){
            this.logger = logger;
        }
        [HttpGet("/sample")]
        [HttpGet("/samples")]
        public ContentResult GetSamples(){
            samples[0]++;
            string json = JsonSerializer.Serialize(samples);
            var result = new ContentResult(){
                StatusCode = 200,
                ContentType = "application/json",
                Content = json
            };
            //logger.LogTrace();
            logger.LogInformation($"Samples value is: {samples[0]}" );
            //logger.LogDebug();
            //logger.LogWarning();
            //logger.LogError();
            //logger.LogCritical();
            return result;
        }
        [HttpPost("/sample")]
        public ContentResult AddSample([FromBody] int sample){
            samples.Add(sample);
            string json = JsonSerializer.Serialize(samples);
            var result = new ContentResult(){
                StatusCode = 200,
                ContentType = "application/json",
                Content = json
            };
            return result;
        }
        [HttpGet("/hellworld")]
        public ContentResult PrintHelloWorld(){
            string json = JsonSerializer.Serialize("hello:world");
            var result = new ContentResult(){
                StatusCode = 200,
                ContentType = "application/json",
                Content = json
            };
            return result;
        }

    }
}