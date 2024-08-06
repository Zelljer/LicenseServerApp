using Newtonsoft.Json;

namespace LicenseServerApp.Models
{
	public class TarifAPI
	{
		public class TarifResponse
		{
            [JsonProperty("id")]
            public int Id { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("program")]
            public string Program { get; set; }
            [JsonProperty("price")]
            public long Price { get; set; }
            [JsonProperty("daysCount")]
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

    public class TarifResult
    {
        [JsonProperty("isSuccsess")]
        public bool IsSuccsess { get; set; }
        [JsonProperty("data")]
        public TarifAPI.TarifResponse Data { get; set; }
        [JsonProperty("errors")]
        public string[] Errors { get; set; }
    }

    public class TarifsResult
    {
        [JsonProperty("isSuccsess")]
        public bool IsSuccsess { get; set; }
        [JsonProperty("data")]
        public List<TarifAPI.TarifResponse> Data { get; set; }
        [JsonProperty("errors")]
        public string[] Errors { get; set; }
    }
}
