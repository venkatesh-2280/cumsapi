using Newtonsoft.Json;
using System.Security.Policy;

namespace STAapi.Models
{
    public class PanValidationModel
    {
        public int dividendgid { get; set; }
        public int compgrpgid { get; set; }
        public DateTime benposdate { get; set; }      

    }

    public class PanRow
    {
        public string PAN { get; set; }
        public string PAN_Status { get; set; }
    }
    public class PanUpdateModel
    {
        public int dividendgid { get; set; }
        public List<PanRow> panList { get; set; }

    }

    public class ExpectedCountRequest
    {
        public int dividendgid { get; set; }
    }

}
