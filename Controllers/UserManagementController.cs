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

        [HttpGet("GetPwdConfigValues")]
        public IActionResult GetPwdConfigValues()
        {
            try
            {
                string constring = _configuration
                    .GetSection("Appsettings")["CMUS_Connection"]
                    .ToString();

                DataSet ds = objData.GetPwdConfigValues(constring);

                if (ds != null &&
                    ds.Tables.Count > 0 &&
                    ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    var result = new
                    {
                        password_max_len = row["password_max_len"]?.ToString(),
                        password_min_len = row["password_min_len"]?.ToString(),
                        pwd_require_uppercase = row["pwd_require_uppercase"]?.ToString(),
                        pwd_require_lowercase = row["pwd_require_lowercase"]?.ToString(),
                        pwd_require_number = row["pwd_require_number"]?.ToString(),
                        pwd_require_special_char = row["pwd_require_special_char"]?.ToString(),
                        password_attempt_count = row["password_attempt_count"]?.ToString()
                    };

                    return Ok(result);
                }

                return Ok(new
                {
                    password_max_len = "",
                    password_min_len = "",
                    pwd_require_uppercase = "",
                    pwd_require_lowercase = "",
                    pwd_require_number = "",
                    pwd_require_special_char = "",
                    password_attempt_count = ""
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = "Failure",
                    message = ex.Message
                });
            }
        }

    }
}
