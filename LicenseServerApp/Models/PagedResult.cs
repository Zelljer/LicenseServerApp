﻿using Newtonsoft.Json;

namespace LicenseServerApp.Models
{
    public class PagedResult<T>
    {
        [JsonProperty("items")]
        public IEnumerable<T> Items { get; set; }
        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }
        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }
    }
}
