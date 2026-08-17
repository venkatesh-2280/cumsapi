using Microsoft.VisualBasic;

namespace STAapi.Models
{
    public class SecurityTypeModel
    {
        public int sectype_gid { get; set; }
        public int sectype_presale_gid { get; set; }
        public string? sectype_security_type { get; set; }
        public decimal sectype_paidup_captital { get; set; }
        public decimal sectype_face_value { get; set; }
        public int sectype_no_of_shares { get; set; }
        public int sectype_no_of_shareholders { get; set; }
        public int sectype_tenure { get; set; }
        public string? sectype_service_start_month { get; set; }
        public string? sectype_isin_no { get; set; }
        public DateTime? sectype_isin_start_date { get; set; }
        public decimal sectype_amount { get; set; }
        public string? sectype_description { get; set; }
        public string? queuestatus { get; set; }
        public int insert_by { get; set; }
        public int update_by { get; set; }
        public string? action { get; set; }
        public int sectype_version { get; set; }
    }
}
