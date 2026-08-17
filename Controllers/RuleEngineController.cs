using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using STAapi.Models;
using STAReportsAPI.STADataAccess;
using System.Data;

namespace STAapi.Controllers
{
    public class RuleEngineController : Controller
    {
        private IConfiguration _configuration;
        public RuleEngineData objData = new RuleEngineData();
        public RuleEngineController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string constring = "";

        [HttpPost("RuleEngineList")]
        public IActionResult RuleEngineList([FromBody] RuleEngineListRequest insObj)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getruleenginelist(insObj, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }

        }

        [HttpPost("IudRuleEngine")]
        public IActionResult IudRuleEngine([FromBody] RuleEngineModel insObj)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.IudruleEngine(insObj, constring);
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
