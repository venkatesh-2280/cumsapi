using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using STAapi.Models;
using STAReportsAPI.STADataAccess;
using System.Data;

namespace STAapi.Controllers
{
    public class PanValidationController : Controller
    {
        private IConfiguration _configuration;
        public PanValidationData objData = new PanValidationData();
        public PanValidationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string constring = "";

        [HttpPost("PanValidationProcess")]
        public IActionResult PanValidationProcess([FromBody] PanValidationModel insObj)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getpandetails(insObj, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }

        }

        [HttpPost("PanUpdateProcess")]
        public IActionResult PanUpdateProcess([FromBody] PanUpdateModel insObj)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();

            DataSet response = new DataSet();

            try
            {
                response = objData.getpanupdatedetails(insObj, constring);

                var result = JsonConvert.SerializeObject(response, Formatting.None);

                return Ok(new
                {
                    success = true,
                    message = "Bulk PAN Status Updated Successfully",
                    data = result
                });
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    success = false,
                    message = e.Message
                });
            }
        }


        [HttpPost("GetExpectedPanCount")]
        public IActionResult GetExpectedPanCount([FromBody] ExpectedCountRequest request)
        {
            try
            {
                constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();

                int expectedCount = objData.GetExpectedPanCount(request.dividendgid, constring);

                return Ok(new
                {
                    success = true,
                    expectedCount = expectedCount
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
