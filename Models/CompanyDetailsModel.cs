using Newtonsoft.Json;
using System.Security.Policy;

namespace STAapi.Models
{
    public class CompanyDetailsModel
    {
        public int compgrpgid { get; set; }
        public string companyName { get; set; }
        public string isin { get; set; }

    }
}
