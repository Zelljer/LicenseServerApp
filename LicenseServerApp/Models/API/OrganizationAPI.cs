using LicenseServerApp.Models.API.Dependencies;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LicenseServerApp.Models.API
{
    public class OrganizationAPI
    {
        public class OrganizationResponse
        {
            [JsonProperty("id")]
            public int Id { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("inn")]
            public string Inn { get; set; }
            [JsonProperty("kpp")]
            public string? Kpp { get; set; }
            [JsonProperty("email")]
            public string Email { get; set; }
            [JsonProperty("phone")]
            public string Phone { get; set; }
        }
        public class OrganizationRequest
        {
			[JsonProperty("name")]
			public string Name { get; set; }
			[JsonProperty("inn")]
            public string Inn { get; set; }
            [JsonProperty("kpp")]
            public string? Kpp { get; set; }
            [JsonProperty("email")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            [JsonProperty("phone")]
            public string Phone { get; set; }
        }
    }

    public class OrganizationsResult
    {
        [JsonProperty("isSuccsess")]
        public bool IsSuccsess { get; set; }
        [JsonProperty("data")]
        public PagedResult<OrganizationsLiceses> Data { get; set; }
        [JsonProperty("errors")]
        public string[] Errors { get; set; }
    }

    public class OrganizationResult
    {
        [JsonProperty("isSuccsess")]
        public bool IsSuccsess { get; set; }
        [JsonProperty("data")]
        public OrganizationAPI.OrganizationResponse Data { get; set; }
        [JsonProperty("errors")]
        public string[] Errors { get; set; }
    }
}
