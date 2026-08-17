using System.ComponentModel.DataAnnotations;

namespace STAReportsAPI.Models
{
    public class Company_Model
    {


    }
    public class MailConfiguration_Model
    {
        public string ActionName { get; set; }
        public Int32 Gid { get; set; }
        [Required(ErrorMessage = "Please Enter Email Address")]
      
        public string MailId { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Email Password")]
        public string MailPwd { get; set; }
        [Required(ErrorMessage = "Please Enter Email SMTP")]
        public string MailSmtp { get; set; }
        [Required(ErrorMessage = "Please Enter Email Port")]
        public string MailPort { get; set; }
        // [Required(ErrorMessage = "Please Enter Mail Type")]
        public string MailType { get; set; }
        public string CreateBy { get; set; }
        public string CompanyGid { get; set; }
    }
    public class Capital_Model
    {
        public string ActionName { get; set; }
        public Int32 cap_gid { get; set; }
        public Int32 Company_Gid { get; set; }
        public Int32 ShareTypeGid { get; set; }
        public Decimal ShareQty { get; set; }
        public Decimal PaidUpValue { get; set; }
        public Decimal ShareValue { get; set; }
        public string ISIN { get; set; }
        public Int32 User_GID { get; set; }
    }

}
