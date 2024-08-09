using Newtonsoft.Json;

namespace LicenseServerApp.Models.View
{
    public class OrganizationView
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("organizationId")]
        public int OrganizationId { get; set; }
        [JsonProperty("organizationName")]
        public string OrganizationName { get; set; }
        [JsonProperty("organizationInn")]
        public string OrganizationInn { get; set; }
        [JsonProperty("organizationKpp")]
        public string? OrganizationKpp { get; set; }
        [JsonProperty("organizationEmail")]
        public string OrganizationEmail { get; set; }
        [JsonProperty("organizationPhone")]
        public string OrganizationPhone { get; set; }
        [JsonProperty("licenceId")]
        public int LicenceId { get; set; }
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
