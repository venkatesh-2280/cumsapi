using Org.BouncyCastle.Utilities;

namespace STAapi.Models
{
    public class PresaleModel
    {
        public int? presale_gid { get; set; }
        public string? presale_company_name { get; set; }
        public string? presale_contact_person { get; set; }
        public Int64? presale_contact_no { get; set; }
        public string? presale_email_id { get; set; }
        public string? presale_company_addr { get; set; }
        public string? presale_depository_type {  get; set; }
        public string? presale_designation {  get; set; }
        public string? presale_cin {  get; set; }
        public string? presale_company_type { get; set; }

        public string? presale_listing_status { get; set; }
        public string? presale_gst_no { get; set; }
        public string? presale_ref_by { get; set; }
        public Int64? presale_ref_contact_no { get; set; }
        public string? presale_ref_email { get; set; }
        public string? presale_status { get; set; }

        public int? sectype_tenure { get; set; }
        public DateTime? sectype_service_start_month { get; set; }
        public DateTime? sectype_service_end_month { get; set; }
        public int sectype_version { get; set; }
        public string? user_role { get; set; }
        public string? doc_presales_gid { get; set; }
        public string? doc_gst_add { get; set; }
        public DateTime? doc_date { get; set; }
        public int? doc_version { get; set; }
        public string? doc_versions { get; set; }

        public string? presale_ops_status { get; set; }
		public string? presale_sec_type { get; set; }
        public string? presale_secsub_type { get; set; }
        public DateTime? presale_maturity_date { get; set; }
        public int presale_version { get; set; }
		public int? insert_by { get; set; }
        //public DateTime presale_insert_date { get; set; }
        public int? update_by { get; set; }
        public string? action { get; set; }
        public string? presale_add2 { get; set; }
        public string? presale_add3 { get; set; }
        public string? presale_country { get; set; }
        public string? presale_state { get; set; }
        public string? presale_city { get; set; }
        public string? presale_pincode { get; set; }

        public int? doc_gid { get; set; }
        public int? doc_presale_gid { get; set; }
        public int? doc_sectype_gid { get; set; }
        public string? doc_security_type { get; set; }
        public string? doc_type { get; set; }
        public string? doc_file_name { get; set; }
        public string? doc_path { get; set; }
        public string? doc_remarks { get; set; }
        public int? doc_insert_by { get; set; }
        public int? doc_update_by { get; set; }
        public int? queuestatus { get; set; }
    }

    public class mailModel
    {
        public int? presale_gid { get; set; }
        public int presale_version { get; set; }

    }

    public class mailinsertModel
    {
        public string doc_gid { get; set; }
        public string doc_file_name { get; set; }
        public string mail_to { get; set; }
        public string mail_cc { get; set; }
        public string mail_file_name { get; set; }
        public string mail_sub { get; set; }
        public string mail_msg { get; set; }

    }

    public class isinmailinsertModel
    {
        public string? doc_gid { get; set; }
        public string? doc_file_name { get; set; }
        public string? mail_to { get; set; }
        public string? mail_cc { get; set; }
        public string? mail_sub { get; set; }
        public string? mail_msg { get; set; }
        public string? mail_file_name { get; set; }
        public string? mail_file_path { get; set; }
        public int? presale_gid { get; set; }
        public int? chrg_gid { get; set; }
        public int? doctype_gid { get; set; }
        public int? docsubtype_gid { get; set; }
        public int? insert_by { get; set; }

    }

    public class isinservicecharges
    {
        public int? chrg_gid { get; set; }
        public int? chrg_presale_gid { get; set; }
        public int? docsub_gid { get; set; }
        public int? doc_gid { get; set; }
        public int? chrg_gst_percentage { get; set; }
        public decimal? chrg_total_amt { get; set; }
        public decimal? chrg_gst_amt { get; set; }
        public int? chrg_share_count { get; set; }
        public decimal? chrg_payable_amt { get; set; }
        public decimal? chrg_received_amt { get; set; }
        public decimal? chrg_due_amt { get; set; }
        public string? doc_name { get; set; }
        public string? docsub_name { get; set; }
        public DateTime? chrg_date { get; set; }
        public DateTime? chrg_received_date { get; set; }
        public string? chrg_status { get; set; }
        public string? presale_company_name { get; set; }
        public string? presale_depository_type { get; set; }
        public string? isin_no { get; set; }
        public string? chrg_doc_status { get; set; }
        public int? insert_by { get; set; }
        public int? update_by { get; set; }
        public string? in_action { get; set; }
        public string? chrg_remarks { get; set; }
    }

    public class isinservicechargesdoc
    {
        public int? chrgdoc_gid { get; set; }
        public int? chrgdoc_chrg_gid { get; set; }
        public int? chrgdoc_doc_gid { get; set; }
        public int? chrgdoc_docsub_gid { get; set; }
        public string? chrgdoc_file_name { get; set; }
        public string? chrgdoc_path { get; set; }

        public string? doc_extension { get; set; }
        public string? chrgdoc_docno { get; set; }
        public string? remarks { get; set; }
        public string? chrgdoc_doc_type { get; set; }
        public string? chrgdoc_invno { get; set; }
        public DateTime? chrg_invdate { get; set; }
        public int? insert_by { get; set; }
        public int? update_by { get; set; }
        public string? in_action { get; set; }
    }

    public class auditlog
    {
        public int? chrg_presale_gid { get; set; }
        public string? name { get; set; }
        public string? user_role { get; set; }
        public string? log_chrg_status { get; set; }
        public string? log_doc_status { get; set; }
        public string? log_remarks { get; set; }
    }

}
