using System.Data;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using STAapi.Models;
using STAapi.STADataAccess;
using STAReportsAPI.STADataAccess;

namespace STAapi.Controllers
{
    public class DocumentController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DocumentController));
        private IConfiguration _configuration;
        public DocumentData objData = new DocumentData();
        public DocumentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string constring = "";

        [HttpGet("GetDocList")]
        public IActionResult GetDocList(string presaleid)
        {

            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getdoclist(presaleid, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpPost("IudDoc")]
        public IActionResult Iuddoc([FromBody] DocumentModel iudObj)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.iuddoc(iudObj, constring);
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
