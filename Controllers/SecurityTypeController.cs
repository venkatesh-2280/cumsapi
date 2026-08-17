using System.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using STAapi.Models;
using STAapi.STADataAccess;
using STAReportsAPI.Models;

namespace STAapi.Controllers
{
    public class SecurityTypeController : Controller
    {
        private IConfiguration _configuration;
        public SecurityTypeData objData = new SecurityTypeData();
        public SecurityTypeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string constring = "";

        [HttpGet("Sectypelist")]
        public IActionResult GetSectypeList(string presaleid,string presaleversion)
        {

            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getsectype(presaleid, presaleversion, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e) 
            {
                return Problem(title: e.Message);
            }
        }

        [HttpPost("IudSectype")]
        public IActionResult IudSectype([FromBody] SecurityTypeModel mymodel)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.iudsectype(mymodel, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }
    }
}
