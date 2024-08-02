using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LicenseServerApp.Models
{
	public class OrganizationAPI
	{
		public class OrganizationResponse
		{
			public int Id { get; set; }
			public string Inn { get; set; }
			public string? Kpp { get; set; }
			[DataType(DataType.EmailAddress)]
			public string Email { get; set; }
			public string Phone { get; set; }
		}
		public class OrganizationRequest
		{
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
}
