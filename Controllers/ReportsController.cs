using System.Data;
using System.Globalization;
using System.Numerics;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using STAapi.Controllers;
using STAReportsAPI.Models;
using STAReportsAPI.STADataAccess;

namespace STAReportsAPI.Controllers
{
	//[Route("Reports")]
	public class ReportsController : Controller
	{
        private static readonly ILog log = LogManager.GetLogger(typeof(DocumentController));
        private IConfiguration _configuration;
		public ReportsData objData = new ReportsData();
		public ReportsController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		string constring = "";

        [HttpGet("GetMisReport")]
        public IActionResult GetMisReport(string start_date,string end_date)
        {

            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getmisreport(start_date, end_date, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpGet("getCompanyRptDetails")]
        public IActionResult getAuditdetails(int line_no, string start_date, string end_date)
        {

            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getcompanydetails(line_no, start_date, end_date,constring);
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
