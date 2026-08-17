namespace STAapi.Models
{
    public class DocumentModel
    {
        public int? doc_gid { get; set; }
        public int? doc_presale_gid { get; set; }
        public int? doc_sectype_gid { get; set; }
        public string? doc_security_type { get; set; }
        public string? doc_type { get; set; }
        public string? doc_file_name { get; set; }
        public string? doc_path { get; set; }
        public string? doc_remarks { get; set; }
        public int? queuestatus { get; set; }
        public int? doc_insert_by { get; set; }
        public int? doc_update_by { get; set; }
        public string? action { get; set; }
        public int? doc_version { get; set; }
        public string? doc_gst_add { get; set; }
		public DateTime? doc_date { get; set; }
        public string? doc_presales_gid {  get; set; }
        public string? doc_versions { get; set; }
	}
}
