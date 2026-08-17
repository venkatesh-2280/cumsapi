using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using STAReportsAPI.Models;
using STAReportsAPI.STADataAccess;
using System.Data;
using System.Globalization;
using System.Numerics;
using static STAReportsAPI.Models.UserGroups_Model;
namespace STAReportsAPI.Controllers
{
    public class UserGroupsMappingController : Controller
    {
        private IConfiguration _configuration;
        public UserGroupsData objData = new UserGroupsData();
        public UserGroupsMappingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string constring = "";
       
       
        [HttpGet("UserGroups")]
        public IActionResult UserGroups()
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            string cmus_constring = _configuration.GetSection("Appsettings")["CMUS_Connection"].ToString();

            List<UserGroups_Model.UserEntities> usergroups = new List<UserGroups_Model.UserEntities>();
            DataSet response = new DataSet();
            try
            {
                response = objData.fetchUserGroups(cmus_constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }
        [HttpGet("RoleMapping")]
        public IActionResult RoleMapping(string role_code, string app_code)
        {
            constring = _configuration.GetSection("Appsettings")["CMUS_Connection"].ToString();

            List<UserGroups_Model.UserEntities> usergroups = new List<UserGroups_Model.UserEntities>();
            DataSet response = new DataSet();
            try
            {
                response = objData.fetchRoleMapping(role_code,app_code,constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }
        [HttpPost("CreateUserGroupsNewdfd")]
        public IActionResult CreateUserGroupsNew_old(string usergroup_gid, string usergroup_name, string usergroup_code,string app_code,string usrgrpstatus)
        {
            constring = _configuration.GetSection("Appsettings")["CMUS_Connection"].ToString();

            List<UserGroups_Model.UserEntities> usergroups = new List<UserGroups_Model.UserEntities>();
            DataSet response = new DataSet();
            try
            {
                response = objData.CreateUserGroups(Convert.ToInt32(usergroup_gid), usergroup_name,"0" ,usergroup_code, app_code, usrgrpstatus, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpPost("CreateUserGroupsNew")]
        public IActionResult CreateUserGroupsNew(
            string role_id,
            string role_name,
            string role_code,
            string user_id,
            string app_code,
            string role_status)
        {
            try
            {
                string constring = _configuration
                    .GetSection("Appsettings")["CMUS_Connection"];

                var response = objData.CreateUserGroups(
                    Convert.ToInt32(role_id),
                    role_name,
                    user_id,
                    role_code,
                    app_code,
                    role_status,
                    constring);

                if (response.Tables.Count > 0 &&
            response.Tables[0].Rows.Count > 0)
                {
                    var row = response.Tables[0].Rows[0];

                    return Ok(new
                    {
                        msg = Convert.ToInt32(row["msg"]),
                        result = row["result"].ToString()
                    });
                }
                return BadRequest(new
                {
                    msg = 0,
                    result = "No response from database"
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    msg = 0,
                    result = ex.Message
                });
            }
           
        }

        [HttpPost("SaveUserRoleMap")]
        public IActionResult SaveUserRoleMap([FromBody] List<UserGroups_Model.RoleMappings> roleMappings)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                foreach (UserGroups_Model.RoleMappings rolemap in roleMappings)
                {
                    response = objData.SaveUserRoleMapData(rolemap, constring);
                }
                   
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }


        [HttpGet("Application_List")]
        public IActionResult Application_List()
        {
            string cmus_constring = _configuration.GetSection("Appsettings")["CMUS_Connection"].ToString();
            try
            {
                DataSet response = objData.Application_List(cmus_constring); // Call to DataAccess

                if (response != null && response.Tables.Count > 0)
                {
                    var table = response.Tables[0];

                    var result = new
                    {
                        Table = table.AsEnumerable().Select(row => new
                        {
                            app_code = row["app_code"] != DBNull.Value ? row["app_code"].ToString() : string.Empty,
                            app_name = row["app_name"] != DBNull.Value ? row["app_name"].ToString() : string.Empty,
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

        [HttpPost("SaveRolePermissions")]
        public IActionResult SaveRolePermissions([FromBody] List<RolePermissionDto> roleMappings)
        {
            string constring = _configuration.GetSection("Appsettings")["CMUS_Connection"];

            if (roleMappings == null || roleMappings.Count == 0)
            {
                return BadRequest(new { status = false, message = "No permissions to save." });
            }

            try
            {
                int savedCount = objData.SaveRolePermissions(roleMappings, constring);

                return Ok(new
                {
                    status = true,
                    message = $"{savedCount} permissions saved successfully."
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { status = false, message = e.Message });
            }
        }

    }
}
