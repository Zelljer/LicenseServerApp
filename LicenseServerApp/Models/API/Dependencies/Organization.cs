using Newtonsoft.Json;

namespace LicenseServerApp.Models.API.Dependencies
{
    public class Organization
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("inn")]
        public required string Inn { get; set; }
        [JsonProperty("kpp")]
        public string Kpp { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
    }
}
