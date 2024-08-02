using Newtonsoft.Json;

namespace LicenseServerApp.Models
{
	public class TarifAPI
	{
		public class TarifResponse
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public string Program { get; set; }
			public long Price { get; set; }
			public int DaysCount { get; set; }
		}
		public class TarifRequest
		{
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("program")]
            public int Program { get; set; }
			[JsonProperty("price")]
            public long Price { get; set; }
            [JsonProperty("daysCount")]
            public int DaysCount { get; set; }
		}
	}
}
