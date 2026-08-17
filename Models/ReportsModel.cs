namespace STAReportsAPI.Models
{
	public class ReportsModel
	{
		public string start_date { get; set; }
		public string end_date { get; set; }

        public int line_no { get; set; }

        public string presale_company_name { get; set; }
        public string presale_depository_type { get; set; }
        public string presale_sec_type { get; set; }
        public string presale_ops_status { get; set; }

        public string presale_status { get; set; }
        public string presale_ref_by { get; set; }
        public string presale_ref_email { get; set; }
        public string presale_ref_contact_no { get; set; }

        public string presale_insert_date { get; set; }
        public string presale_update_date { get; set; }
        public string received_amount { get; set; }
        public string received_date { get; set; }
        public string sectype_tenure { get; set; }
        public string sectype_service_start_month { get; set; }
        public string sectype_service_end_month { get; set; }
        public string isin_number { get; set; }
        public string gst_number { get; set; }
    }
}
