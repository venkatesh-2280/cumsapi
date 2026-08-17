using System.Data;
using System.Globalization;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using STAReportsAPI.Models;
using STAReportsAPI.STADataAccess;
using static STAReportsAPI.Models.UserGroups_Model;

namespace STAReportsAPI.Controllers
{
    public class EmployeeMasterController : Controller
    {
        private IConfiguration _configuration;
        public EmployeeMasterData objData = new EmployeeMasterData();
        public EmployeeMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string constring = "";



        [HttpGet("EmployeeDetails")]
        public IActionResult EmployeeDetails(string userid, string sAction)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();

            DataSet response = new DataSet();
            try
            {
                response = objData.EmployeeDetails(Convert.ToInt32(userid), sAction, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }
        [HttpGet("GetTreeNodes")]
        public IActionResult GetTreeNodes()
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();

            DataSet response = new DataSet();
            try
            {
                response = objData.GetTreeNodes(constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }
        [HttpGet("GetEmpcheckedvaluedb")]
        public IActionResult GetEmpcheckedvaluedb(string empid)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();

            DataSet response = new DataSet();
            try
            {
                response = objData.GetEmpcheckedvaluedb(empid,constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }
        [HttpPost("SaveEmployee")]
        public IActionResult SaveEmployee([FromBody] EmployeeMaster_Model EDtlObj)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {

                response = objData.SaveEmployee(EDtlObj, constring);


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
