namespace STAapi.Models
{
    public class SecurityTypeDtlModel
    {
        public int sectypdtl_gid { get; set; }
        public int sectypdtl_sectype_gid { get; set; }
        public int sectypdtl_gst_percentage { get; set; }
        public decimal sectypdtl_gst_amount { get; set; }
        public decimal sectypdtl_total_amount { get; set; }
        public decimal sectypdtl_received_amount { get; set; }
        public decimal sectypdtl_due_amount { get; set; }
        public string? sectypdtl_received_date { get; set; }
        public decimal sectypdtl_payable_amount { get; set; }
        public string sectypdtl_description { get; set; }
        public string sectypdtl_remarks { get; set; }
        public int sectypdtl_version { get; set; }
        public int queuestatus { get; set; }
        public int insert_by { get; set; }
        public int update_by { get; set; }
        public string action { get; set; }
    }
}
