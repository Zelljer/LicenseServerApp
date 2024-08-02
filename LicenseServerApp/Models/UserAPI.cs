using Newtonsoft.Json;
using System.Text.Json;

namespace LicenseServerApp.Models
{
	public class UserAPI
	{
		public class UserResponse
		{
			public int Id { get; set; }
			public string Login { get; set; }
			public string Password { get; set; }
			public string Name { get; set; }
			public string Surname { get; set; }
			public string Patronymic { get; set; }
			public string Role { get; set; }
			public string Token { get; set; }
		}

		public class UserRegistrationRequest
		{
			[JsonProperty("login")]
			public string Login { get; set; }

            [JsonProperty("password")]
            public string Password { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("surname")]
            public string Surname { get; set; }

            [JsonProperty("patronymic")]
            public string Patronymic { get; set; }

            [JsonProperty("role")]
            public int Role { get; set; }
        }
		public class UserAuthentificationRequest
		{
			[JsonProperty("login")]
			public string Login { get; set; }

			[JsonProperty("password")]
			public string Password { get; set; }
		}
	}
}

