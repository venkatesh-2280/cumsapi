using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAapimodels
{
    public class headerValue
    {
        public string user_code { get; set; } = "";
        public string role_code { get; set; } = "";
        public string lang_code { get; set; } = "";
        public string ip_address { get; set; } = "";
    }
    public class QcdmasterModel
	{
		public string in_user_code { get; set; }

	}

	public class Qcdgridread
	{
		public string in_user_code { get; set; }
		public string in_master_code { get; set; }
	}

	public class mainQCDMaster
	{
		public string action { get; set; }
		public string active_status { get; set; }
		public string masterCode { get; set; }
		public int masterGid { get; set; }
		public string mastermutiplename { get; set; }
		public string masterName { get; set; }
		public string masterShortCode { get; set; }
		public string masterSyscode { get; set; }
		public string ParentMasterSyscode { get; set; }
		public string depend_parent_master_syscode { get; set; }
		public string depend_master_syscode { get; set; }
		public string depend_flag { get; set; }
	}
}
