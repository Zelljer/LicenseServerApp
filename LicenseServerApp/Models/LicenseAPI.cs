using Newtonsoft.Json;

namespace LicenseServerApp.Models
{
	public class LicenseAPI
	{
		public class LicenseResponse
		{
			public int Id { get; set; }
			public int OrganizationId { get; set; }
			public int TarifId { get; set; }
			public DateTime DateCreated { get; set; }
			public DateTime StartDate { get; set; }
			public DateTime EndDate { get; set; }
		}

		public class LicenseRequest
		{
            [JsonProperty("organizationId")]
            public int OrganizationId { get; set; }
            [JsonProperty("tarifId")]
            public int TarifId { get; set; }
            [JsonProperty("dateStart")]
            public string DateStart { get; set; }
		}
	}
}
