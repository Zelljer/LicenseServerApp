using Newtonsoft.Json;

namespace LicenseServerApp.Models.View
{
    public class LicenseView
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("licenceId")]
        public int LicenceId { get; set; }
        [JsonProperty("organizationName")]
        public string OrganizationName { get; set; }
        [JsonProperty("programName")]
        public string ProgramName { get; set; }
        [JsonProperty("tarifName")]
        public string TarifName { get; set; }
        [JsonProperty("dateCreated")]
        public string DateCreated { get; set; }
        [JsonProperty("startDate")]
        public string StartDate { get; set; }
        [JsonProperty("endDate")]
        public string EndDate { get; set; } 
    }
}
