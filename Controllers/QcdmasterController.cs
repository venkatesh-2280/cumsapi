using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using STAapimodels;
using STAReportsAPI.STADataAccess;
namespace STAapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QcdmasterController : ControllerBase
	{
		private IConfiguration _configuration;
		public QcdmasterController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		string constring = "";

		[HttpPost("QcdMasterRead")]
		public IActionResult QcdMasterRead(QcdmasterModel qcdmodel)
		{
            int ico = 0;
            int icount = 10 / ico;
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
			try
			{
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = QcdmasterService.QcdMasterRead(qcdmodel, header_value, constring);
				var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
				return Ok(serializedProduct);
			}
			catch (Exception e)
			{
				return Problem(title: e.Message);
			}
		}

		[HttpPost("QcdMasterGridRead")]
		public IActionResult QcdMasterGridRead(Qcdgridread objgridread)
		{
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
			try
			{
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = QcdmasterService.QcdMasterGridRead(objgridread, header_value, constring);
				var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
				return Ok(serializedProduct);
			}
			catch (Exception e)
			{
				return Problem(title: e.Message);
			}
		}

		[HttpPost("QcdMaster")]
		public IActionResult QcdMaster(mainQCDMaster objmaster)
		{
			//int ico = 0;
			//int icount = 10 / ico;
			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
			try
			{
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				try
				{
					response = QcdmasterService.QcdMasters(objmaster, header_value, constring);
					var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
					return Ok(serializedProduct);
				} catch (Exception ex)
				{
					return Problem(title: ex.Message);
				}
				
			}
			catch (Exception e)
			{
				return Problem(title: e.Message);
			}
		}

		[HttpGet("getallqcdmaster1")]
		public IActionResult getallqcdmaster2()
		{
            return Ok("serializedProduct");
        }

        [HttpPost("getallqcdmaster")]
		public IActionResult getallqcdmaster(Qcdgridread objgridread)
		{
            //int ico = 0;
            //int icount = 10 / ico;
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			headerValue header_value = new headerValue();
			DataTable response = new DataTable();
			try
			{
				var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
				var getlangCode = Request.Headers.TryGetValue("lang_code", out var lang_code) ? lang_code.First() : "";
				var getRoleCode = Request.Headers.TryGetValue("role_code", out var role_code) ? role_code.First() : "";
				header_value.user_code = getvalue;
				header_value.lang_code = getlangCode;
				header_value.role_code = getRoleCode;
				response = QcdmasterService.getallqcdservice(objgridread, header_value, constring);
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
