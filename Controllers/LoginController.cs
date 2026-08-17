using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Serilog;
using STAapi.Controllers;
using STAapi.Models;
using STAReportsAPI.Models;
using STAReportsAPI.STADataAccess;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace STAReportsAPI.Controllers
{
    public class LoginController : Controller
    {
        private Random _random;
        private IConfiguration _configuration;
        DataSet response = new DataSet();
        public LoginData objData = new LoginData();
        string constring = "";

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("getOTP")]
        public IActionResult getOTP([FromBody] LoginModel objModel)
        {
            try
            {
                if (objModel.empEmail.ToString().Contains("admin") == false)
                {
                    string useremail = string.Empty;
                    // Initialize the Random instance
                    _random = new Random();
                    // Generate a random 6-digit number
                    int randomNumber = _random.Next(100000, 1000000); // Generates a number between 100000 and 999999 (inclusive)

                    constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
                    using (MySqlConnection conn = new MySqlConnection(constring))
                    {
                        conn.Open();
                        string checkUserQuery = "SELECT email FROM gnsa_mst_tuser WHERE emp_code = @Empcode";
                        using (MySqlCommand cmd = new MySqlCommand(checkUserQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Empcode", objModel.empEmail);

                            MySqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                useremail = reader["email"].ToString();
                            }
                            else
                            {
                                // return Json("User not found.");

                            }
                        }
                    }
                    response = objData.setCredentials(useremail, Convert.ToString(randomNumber), constring);
                    var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                    if (Convert.ToInt16(response.Tables[0].Rows[0]["Clear"]) == 1)
                    {
                        // SMTP configuration
                        string smtpServer = Convert.ToString(_configuration.GetSection("Appsettings")["smtpServer"]);
                        int smtpPort = Convert.ToInt16(_configuration.GetSection("Appsettings")["smtpPort"]);
                        string smtpUsername = Convert.ToString(_configuration.GetSection("Appsettings")["smtpUsername"]); // Update with your Gmail address
                        string smtpPassword = Convert.ToString(_configuration.GetSection("Appsettings")["smtpPassword"]); // Update with the App Password generated

                        // Create a MailMessage object
                        MailMessage message = new MailMessage();
                        message.From = new MailAddress(smtpUsername);
                        message.To.Add(new MailAddress(useremail)); // Update with the recipient's email
                        message.Subject = Convert.ToString(_configuration.GetSection("Appsettings")["subject"]);
                        message.Body = Convert.ToString(_configuration.GetSection("Appsettings")["body"]) + randomNumber;

                        // Create a SmtpClient object and send the email
                        using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                        {
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                            smtpClient.EnableSsl = true; // Enable SSL/TLS encryption
                            smtpClient.Send(message);
                        }
                    }
                    return Ok(serializedProduct);
                }
                else
                {
                    DataTable dataTable = new DataTable();

                    // Add columns to the DataTable
                    dataTable.Columns.Add("Msg", typeof(string));
                    dataTable.Columns.Add("Clear", typeof(int));

                    // Add a row to the DataTable
                    DataRow row = dataTable.NewRow();
                    row["Msg"] = "Enter Admin Password.!";
                    row["Clear"] = 1;
                    dataTable.Rows.Add(row);
                    response.Tables.Add(dataTable);
                    var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                    return Ok(serializedProduct);
                }


            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }

        }

        [HttpPost("validateOTP")]
        public IActionResult validateOTP([FromBody] LoginModel objModel)
        {
            try
            {
                constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
                response = objData.validateCredentials(objModel.empEmail, objModel.Otp, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }

        }

        [HttpPost("getMenu")]
        public IActionResult getMenu([FromBody] LoginModel objModel)
        {
            try
            {
                constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
                response = objData.GetMenu(objModel.userGroupid, objModel.menuType, objModel.menuGid, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }

        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword([FromBody] LoginModel Obj)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.chngpwd(Obj, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpPost("GetChngPwdFlag")]
        public IActionResult GetChngPwdFlag([FromBody] LoginModel iudObj)
        {

            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.User_loginvalidate(iudObj, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }


        private string GenerateJwtToken(string email, string userId, string role)
        {
            var issuer = AuthController.Decrypt(_configuration["Jwt:Issuer"]);
            var audience = AuthController.Decrypt(_configuration["Jwt:Audience"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(
                    Convert.ToInt32(_configuration["Jwt:ExpiryMinutes"])
                ),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost("User_loginvalidate")]
        public IActionResult User_loginvalidate([FromBody] LoginModel iudObj)
        {

            try
            {
                string constring = _configuration["Appsettings:CMUS_Connection"];
                DataSet ds = objData.User_loginvalidate(iudObj, constring);

                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    return Unauthorized("Invalid credentials");

                DataRow row = ds.Tables[0].Rows[0];

                if (Convert.ToInt32(row["id"]) <= 0)
                    return Unauthorized("Invalid credentials");


                string tokens = GenerateJwtToken(
                    row["email"].ToString(),
                    row["id"].ToString(),
                    row["user_role"].ToString()
                );
                return Ok(new
                {
                    token = tokens,
                    User = new
                    {
                        Id = row["id"],
                        Name = row["name"],
                        Email = row["email"],
                        Role = row["user_role"],
                        user_code = row["emp_code"],
                        PasswordChangedFlag = row["password_changed_flag"],
                        MandatoryFieldId = row["mandatory_field_id"],
                        min_pwd_length = row["min_pwd_length"],
                        max_pwd_length = row["max_pwd_length"],
                        require_uppercase = row["require_uppercase"],
                        require_lowercase = row["require_lowercase"],
                        require_number = row["require_number"],
                        require_special_char = row["require_special_char"]
                    }
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Login failed");
                return StatusCode(500, "Internal server error");
            }
        }


        [AllowAnonymous]
        [HttpPost("ChkLogin")]
        public IActionResult ChkLogin([FromBody] LoginModel iudObj)
        {

            constring = _configuration.GetSection("Appsettings")["CMUS_Connection"].ToString();
            string IPO_constring = _configuration.GetSection("Appsettings")["IPO_Connection"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.Users_login(iudObj, constring, IPO_constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("UsersLogout")]
        public IActionResult UsersLogout([FromBody] LoginModel iudObj)
        {
            string IPO_constring = _configuration.GetSection("Appsettings")["IPO_Connection"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                objData.GetLogindetails(iudObj, IPO_constring, "Logout", iudObj.action);
                return Ok("Logout");
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

    }
}
