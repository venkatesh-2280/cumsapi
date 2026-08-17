using System.Data;
using System.Globalization;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using gnsastaapi.Models;
using STAReportsAPI.STADataAccess;
using static STAReportsAPI.Models.UserGroups_Model;

namespace STAapi.Controllers
{
    public class UserManagementController : Controller
    {
        private IConfiguration _configuration;
        public UserManagementData objData = new UserManagementData();
        public UserManagementController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string constring = "";
        string cmus_constring = "";

        [HttpGet("UsersList")]
        public IActionResult UsersList()
        {
            // string constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            string cmus_constring = _configuration.GetSection("Appsettings")["CMUS_Connection"].ToString();
            try
            {
                DataSet response = objData.getuserlist(cmus_constring); // Call to DataAccess

                if (response != null && response.Tables.Count > 0)
                {
                    var table = response.Tables[0];

                    var result = new
                    {
                        Table = table.AsEnumerable().Select(row => new
                        {
                            id = row["id"] != DBNull.Value ? row["id"].ToString() : string.Empty,
                            empcode = row["emp_code"] != DBNull.Value ? row["emp_code"].ToString() : string.Empty,
                            name = row["name"] != DBNull.Value ? row["name"].ToString() : string.Empty,
                            usermobile = row["mobileno"] != DBNull.Value ? row["mobileno"].ToString() : string.Empty,
                            isclient = row["is_client"] != DBNull.Value ? row["is_client"].ToString() : string.Empty,
                            userpwd = row["userpwd"] != DBNull.Value ? row["userpwd"].ToString() : string.Empty,
                            userotp = row["otp"] != DBNull.Value ? row["otp"].ToString() : string.Empty,
                            email = row["email"] != DBNull.Value ? row["email"].ToString() : string.Empty,
                            role_code = row["userpwdexpdays"] != DBNull.Value ? row["userpwdexpdays"].ToString() : string.Empty,
                            role_name = row["lock_flag"] != DBNull.Value ? row["lock_flag"].ToString() : string.Empty,
                            status = row["status"] != DBNull.Value ? row["status"].ToString() : string.Empty,
                            userpwdexpdays = row["userpwdexpdays"] != DBNull.Value ? row["userpwdexpdays"].ToString() : string.Empty,
                            lock_flag = row["lock_flag"] != DBNull.Value ? row["lock_flag"].ToString() : string.Empty
                        })
                    };

                    return Ok(result);
                }

                return NotFound("No data found.");
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpPost("InsUser")]
        public IActionResult InsUser([FromBody] UserManagementModel insObj)
        {
            //constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            cmus_constring = _configuration.GetSection("Appsettings")["CMUS_Connection"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.InsUser(insObj, cmus_constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpPost("UpdUser")]
        public IActionResult UpdUser([FromBody] UserManagementModel insObj)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            cmus_constring = _configuration.GetSection("Appsettings")["CMUS_Connection"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.UpdUsers(insObj, cmus_constring);
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
