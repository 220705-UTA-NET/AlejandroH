using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Text.Json;
namespace MyApi.App.Controllers{
    [Route("api/[Controller]")]
    [ApiController]
    public class SampleGetParametersController: ControllerBase{
        //fields
        private readonly ILogger<SampleController> logger;
        private static readonly List<int> samples = new() {12};
        private static Models.DiningTable table = new Models.DiningTable();
        public SampleGetParametersController(ILogger<SampleController> logger){//gets call everytime we make a request
            this.logger = logger;
            table.Address ="123 fake st.";
            table.Description = "floating table";
            table.Id = 101;
            table.HostedBy = "ME";
            table.TableNumber = 4;
            table.PhoneNumber = "123456789";
            logger.LogInformation("We have entered the constructor again");
        }
        [HttpGet("/displayname")]
        public ContentResult GetNameParameter(string name){
            //string name = Request.QueryString("name");
            //string name = Request["name"].ToString();
            logger.LogInformation(name);
            string json = JsonSerializer.Serialize("name:"+name);
            var result = new ContentResult(){
                StatusCode = 200,
                ContentType = "application/json",
                Content = json
            };
            return result;
        }
        [HttpGet("/table")]
        public ContentResult GetTableInfo(){
            string json = JsonSerializer.Serialize(table);
            var result = new ContentResult(){
                StatusCode = 200,
                ContentType = "application/json",
                Content = json
            };
            return result;
        }
        //[HttpPatch("/table")]
        [HttpPost("table")]
        public ContentResult UpdateTable(string? Address, string? Description, int? Id, string? HostedBy, int? TableNumber, string? PhoneNumber){
        //public ContentResult UpdateTable([FromBody] string HostedBy){//}, int? TableNumber, string? PhoneNumber){
            //if(Address != null){
            //    table.Address = Address;
            //}
            //if(Description != null){
            //    table.Description = Description;
            //}
            if(HostedBy != null){
                table.HostedBy = HostedBy;
                
            }
            var result = new ContentResult(){
                StatusCode = 201
            };
            logger.LogInformation("hostedby contains: " + HostedBy);
            logger.LogInformation("object contains: " + table.HostedBy);
            return result;
        } 
    }
}