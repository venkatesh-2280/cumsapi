using System.Data;
using gnsastaapi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using STAapi.Models;
using STAapi.STADataAccess;
using STAReportsAPI.STADataAccess;

namespace STAapi.Controllers
{
    public class PresaleController : Controller
    {

        private IConfiguration _configuration;
        public PresaleData objData = new PresaleData();
        public PresaleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string constring = "";

        [HttpGet("PresaleList")]
        public IActionResult GetPresaleList(string ActionTab)
        {

            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getpresalelist(ActionTab, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }
       

        [HttpGet("GetAuditList")]
		public IActionResult GetAuditList(string presaleid)
		{

			constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
			//headerValue header_value = new headerValue();
			DataSet response = new DataSet();
			try
			{
				response = objData.getauditlist(presaleid, constring);
				var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
				return Ok(serializedProduct);
			}
			catch (Exception e)
			{
				return Problem(title: e.Message);
			}
		}

        [HttpGet("GetPresaleRenewalList")]
        public IActionResult GetPresaleRenewalList()
        {

            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getpresalerenewallist(constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpPost("CheckMailTrigger")]
        public IActionResult CheckMailTrigger([FromBody] mailModel iudObj1)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getmailtrigger(iudObj1, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpPost("insemaildetails")]
        public IActionResult insemaildetails([FromBody] mailinsertModel insObj)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.InsEmailDetails(insObj, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        //[HttpPost("insemaildetails1")]
        //public IActionResult insemaildetails1([FromBody] isinmailinsertModel insObj)
        //{
        //    constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
        //    //headerValue header_value = new headerValue();
        //    DataSet response = new DataSet();
        //    try
        //    {
        //        response = objData.InsEmailDetails1(insObj, constring);
        //        var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
        //        return Ok(serializedProduct);
        //    }
        //    catch (Exception e)
        //    {
        //        return Problem(title: e.Message);
        //    }
        //}

        [HttpPost("insemaildetails1")]
        public IActionResult insemaildetails1([FromBody] isinmailinsertModel insObj)
        {
            string constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            DataSet response = new DataSet();

            try
            {
                response = objData.InsEmailDetails1(insObj, constring);

                // --- Get mail_gid & extension from result ---
                if (response.Tables.Count > 0 && response.Tables[0].Rows.Count > 0)
                {
                    DataRow row = response.Tables[0].Rows[0];
                    string result = row["result"]?.ToString();
                    string mail_gid = row["mail_gid"]?.ToString();
                    string mail_doc_extension = row["mail_doc_extension"]?.ToString();

                    if (!string.IsNullOrEmpty(mail_gid) && !string.IsNullOrEmpty(mail_doc_extension))
                    {
                        // ✅ Define folder paths
                        string folderPath = @"D:\BillingAttachments\isinmailattchments\";

                        // ✅ Get original uploaded file path (first one if multiple)
                        string originalPath = "";
                        if (!string.IsNullOrEmpty(insObj.mail_file_path))
                        {
                            originalPath = insObj.mail_file_path.Split(',')[0].Trim();
                        }

                        // ✅ New file path with mail_gid
                        string newFilePath = Path.Combine(folderPath, mail_gid + mail_doc_extension);

                        // ✅ Copy or rename the file if it exists
                        if (System.IO.File.Exists(originalPath))
                        {
                            //System.IO.File.Copy(originalPath, newFilePath, true);
                            System.IO.File.Move(originalPath, newFilePath);
                        }

                        // ✅ Optional: Update the DB with the new file path
                       //UpdateMailFilePath(Convert.ToInt32(mail_gid), newFilePath, constring);
                    }
                }

                var serialized = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serialized);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }


        [HttpPost("IudPresale")]
        public IActionResult IudPresale([FromBody] PresaleModel iudObj)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.iudpresale(iudObj, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpGet("PresaleISINList")]
        public IActionResult GetPresaleISINList(string ActionTab)
        {

            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getpresaleisinlist(ActionTab, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpGet("GetMstDocList")]
        public IActionResult GetMstDocList(int doc_gid, string in_action, string in_docsub_category)
        {

            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getmstdoclist(doc_gid, in_action, in_docsub_category ,constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpGet("getISINList")]
        public IActionResult getISINList(int chrg_presale_gid, int chrg_gid, string start_date, string end_date, string status)
        {

            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getisinlist(chrg_presale_gid, chrg_gid, start_date,end_date,status, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpGet("getAuditdetails")]
        public IActionResult getAuditdetails(int chrg_presale_gid)
        {

            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getsauditlist(chrg_presale_gid,constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpPost("IudISINSerChrg")]
        public IActionResult IudISINSerChrg([FromBody] isinservicecharges iudObj)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.iudisinserchrg(iudObj, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpGet("GetISINSerChrgDtl")]
        public IActionResult GetISINSerChrgDtl(string chrg_gid)
        {

            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getisinserchrgdtl(chrg_gid, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpGet("GetISINSerChrgDocDtl")]
        public IActionResult GetISINSerChrgDocDtl(int chrgdoc_chrg_gid)
        {

            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getisinserchrgdocdtl(chrgdoc_chrg_gid, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpPost("IudISINSerChrgDoc")]
        public IActionResult IudISINSerChrgDoc([FromBody] isinservicechargesdoc iudObj)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.iudisinserchrgdoc(iudObj, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpGet("GetISINMailDtl")]
        public IActionResult GetISINMailDtl(int isinmail_gid)
        {

            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getisinmaildtl(isinmail_gid, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpGet("GetMstSecList")]
        public IActionResult GetMstSecList (string in_action, int sectype_gid)
        {

            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getmstseclist(in_action, sectype_gid ,constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpGet("GetMstList")]
        public IActionResult GetMstList(string in_action, int country_gid, int state_gid)
        {

            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getmstlist(in_action, country_gid, state_gid, constring);
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
