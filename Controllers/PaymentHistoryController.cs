using System.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using STAapi.STADataAccess;

namespace STAapi.Controllers
{
    public class PaymentHistoryController : Controller
    {
        
        private IConfiguration _configuration;
        public PaymentHistoryData objData = new PaymentHistoryData();
        public PaymentHistoryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string constring = "";

        [HttpGet("PayHislist")]
        public IActionResult PayHislist(string presaleid)
        {

            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getpayhis(presaleid, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpGet("PayHisDtllist")]
        public IActionResult PayHisDtllist(string sectypeid)
        {

            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getpayhisdtl(sectypeid, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        //[HttpGet("PayHisDtllist")]
        //public IActionResult PayHisDtllist(string sectypeid)
        //{

        //    constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
        //    //headerValue header_value = new headerValue();
        //    DataSet response = new DataSet();
        //    try
        //    {
        //        response = objData.getpayhisdtl(sectypeid, constring);
        //        var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
        //        return Ok(serializedProduct);
        //    }
        //    catch (Exception e)
        //    {
        //        return Problem(title: e.Message);
        //    }
        //}

        //[HttpGet("PayHisDtllist")]
        //public IActionResult PayHisDtllist(string sectypeid)
        //{
        //    constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
        //    DataSet response = new DataSet();

        //    try
        //    {
        //        response = objData.getpayhisdtl(sectypeid, constring);

        //        // Ensure DataSet has tables
        //        if (response != null && response.Tables.Count > 0)
        //        {
        //            // Return as JSON object { Table: [rows] }
        //            return Ok(new { Table = response.Tables[0] });
        //        }
        //        else
        //        {
        //            return NotFound("No data found.");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Problem(title: "API Error", detail: e.Message);
        //    }
        //}

    }
}
