using System.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using STAapi.Models;
using STAapi.STADataAccess;

namespace STAapi.Controllers
{
    public class SecurityTypeDtlController : Controller
    {
        private IConfiguration _configuration;
        public SecurityTypeDtlData objData = new SecurityTypeDtlData();
        public SecurityTypeDtlController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string constring = "";

        [HttpGet("GetSectypedtl")]
        public IActionResult GetSectypDtl(string sectype_gid)
        {

            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getsectypdtllist(Convert.ToInt32(sectype_gid), constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpPost("IudSectypedtl")]
        public IActionResult IudSectypeDtl([FromBody] SecurityTypeDtlModel iudObj)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.iudsectypedtl(iudObj, constring);
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
