using Newtonsoft.Json;

namespace battery.app.Core.DTO
{
	public class AuthDto
	{
		[JsonProperty("login")]
		public string Login
		{
			get;
			set;
		}

		[JsonProperty("password")]
		public string Password
		{
			get;
			set;
		}
	}
}
