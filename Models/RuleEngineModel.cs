using Newtonsoft.Json;
using System.Security.Policy;

namespace STAapi.Models
{
    public class RuleEngineModel
    {
        public int rulegid { get; set; }
        public string rulename { get; set; }
        public int tdstype { get; set; }
        public int benecategory { get; set; }
        public int panstatus { get; set; }
        public int pancat { get; set; }
        public decimal tdsrate { get; set; }
        public string user { get; set; }
        public string action { get; set; }

    }

    public class RuleEngineListRequest
    {
        public int rulegid { get; set; }
        public string action { get; set; }
    }

}
