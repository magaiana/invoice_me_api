using Newtonsoft.Json;

namespace BW.Assessment.Infrastructure.Configuration
{
	public class JwtSettings
	{
        [JsonProperty("secret")]
        public string Secret { get; set; }
        [JsonProperty("issuer")]
        public string Issuer { get; set; }
        [JsonProperty("audience")]
        public string Audience { get; set; }
        [JsonProperty("expiry")]
        public int Expiry { get; set; }
        [JsonProperty("refreshExpiry")]
        public int RefreshExpiry { get; set; }
    }
}
