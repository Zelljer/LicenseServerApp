using Newtonsoft.Json;

namespace LicenseServerApp.Models
{
    public class OrganizationsLiceses
    {
        [JsonProperty("organization")]
        public Organization Organization { get; set; }
        [JsonProperty("licenses")]
        public List<LicenseAPI.LicenseResponse> Licenses { get; set; } = new List<LicenseAPI.LicenseResponse>();
    }
}
