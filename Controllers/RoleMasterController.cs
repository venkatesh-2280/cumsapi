using gnsastaapi.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using STAapi.Models;
using STAapi.STADataAccess;
using STAReportsAPI.STADataAccess;
using System.Data;

namespace STAapi.Controllers
{
    public class RoleMasterController : Controller
    {
        private IConfiguration _configuration;
        public RoleMasterData objData = new RoleMasterData();
        string constring = "";
        string cmus_constring = "";
        public RoleMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("RoleList")]
        public IActionResult RoleList()
        {
           string cmus_constring = _configuration.GetSection("Appsettings")["CMUS_Connection"].ToString();
              try
            {
                DataSet response = objData.getroleList(cmus_constring); // Call to DataAccess

                if (response != null && response.Tables.Count > 0)
                {
                    var table = response.Tables[0];

                    var result = new
                    {
                        Table = table.AsEnumerable().Select(row => new
                        {
                            role_gid = row["role_id"] != DBNull.Value ? row["role_id"].ToString() : string.Empty,
                            role_code = row["role_code"] != DBNull.Value ? row["role_code"].ToString() : string.Empty,
                            role_name = row["role_name"] != DBNull.Value ? row["role_name"].ToString() : string.Empty,
                           // application_gid= row["application_id"] != DBNull.Value ? row["application_id"].ToString() : string.Empty,
                            application_code = row["app_code"] != DBNull.Value ? row["app_code"].ToString() : string.Empty,
                            application_name = row["app_name"] != DBNull.Value ? row["app_name"].ToString() : string.Empty,
                             status = row["status"] != DBNull.Value ? row["status"].ToString() : string.Empty
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

        [HttpGet("GetUserrole_Mapping")]
        public IActionResult GetUserrole_Mapping()
        {
            DataTable dt = new DataTable();

            string cmus_constring = _configuration["Appsettings:CMUS_Connection"];

            using (MySqlConnection con = new MySqlConnection(cmus_constring))
            {
                using (MySqlCommand cmd = new MySqlCommand("pr_get_approle_mapping", con))
                {
                     cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            var result = dt.AsEnumerable()
                .Select(row => dt.Columns.Cast<DataColumn>()
                    .ToDictionary(col => col.ColumnName, col => row[col]))
                .ToList();

            return Json(result);
        }


        [HttpPost("SaveUserRoles")]
        public IActionResult SaveUserRoles([FromBody] SaveUserRolesRequest request)
        {
            try
            {
                string constring = _configuration.GetSection("Appsettings")["CMUS_Connection"].ToString();

                var result = objData.SaveUserRoles(request, constring);

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetUserRole")]
        public IActionResult GetUserRole(string userCode)
        {
            DataTable dt = new DataTable();

            string cmus_constring = _configuration["Appsettings:CMUS_Connection"];

            using (MySqlConnection con = new MySqlConnection(cmus_constring))
            {
                using (MySqlCommand cmd = new MySqlCommand("pr_get_userrole_mapping", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure; 
                    cmd.Parameters.AddWithValue("in_user_code", userCode); 
                    con.Open();

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            var result = dt.AsEnumerable()
         .Select(row => new
         {
             app_code = row["app_code"].ToString(),
             role_code = row["role_code"].ToString()
         })
         .ToList();

            return Json(result);
        }


    }
}
