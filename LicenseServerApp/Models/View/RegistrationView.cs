using Newtonsoft.Json;

namespace LicenseServerApp.Models.View
{
	public class RegistrationView
	{
		[JsonProperty("login")]
		public string Login { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }
		[JsonProperty("сheckPassword")]
		public string CheckPassword { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("surname")]
		public string Surname { get; set; }

		[JsonProperty("patronymic")]
		public string Patronymic { get; set; }
	}
}
