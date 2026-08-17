using gnsastaapi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using STAapi.Models;
using STAReportsAPI.STADataAccess;
using System.Data;

namespace STAapi.Controllers
{
    public class DividendMasterController : Controller
    {
        private IConfiguration _configuration;
        public DividendMasterData objData = new DividendMasterData();
        public DividendMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string constring = "";


        [HttpPost("DividendList")]
        public IActionResult DividendList([FromBody] DividendListRequest insObj)           
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getdividendlist(insObj, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        
        }

        [HttpPost("CompDetails")]
        public IActionResult CompDetails([FromBody] CompanyDetailsModel inscObj)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getCompany(inscObj, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpPost("IudDividend")]
        public IActionResult IudDividend([FromBody] DividendMasterModel insObj)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.IudDividend(insObj, constring);
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
