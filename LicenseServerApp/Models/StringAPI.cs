using Newtonsoft.Json;

namespace LicenseServerApp.Models
{
	public class StringResult
    {
        [JsonProperty("isSuccsess")]
        public bool IsSuccsess { get; set; }
        [JsonProperty("data")]
        public string Data { get; set; }
        [JsonProperty("errors")]
        public string[] Errors { get; set; }
    }
}
