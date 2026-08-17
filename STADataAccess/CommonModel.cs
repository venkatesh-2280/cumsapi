namespace gnsastaapi.STADataAccess
{
	public class CommonModel
	{
		public class errorlogModel
		{
			public string in_ip_addr { get; set; }
			public string in_source_name { get; set; }
			public string in_proc_name { get; set; }
			public string in_errorlog_text { get; set; }
			public string user_code { get; set; }

		}

		public class configvalueModel
		{
			public string in_config_name { get; set; }

		}
		public class roleconfig
		{
			public int in_screen_code { get; set; }
			public string add { get; set; }
			public string edit { get; set; }
			public string view { get; set; }
			public string delete { get; set; }
			public string process { get; set; }
			public string download { get; set; }
			public string deny { get; set; }
		}
	}
}
