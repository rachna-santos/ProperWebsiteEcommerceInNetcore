using Newtonsoft.Json;

namespace Theme_Implemenet.Models
{
    public class GoogleRefreshTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        /// <summary>
        /// Use to get new token
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        /// <summary>
        /// Measured in seconds
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        /// <summary>
        /// Should always be "Bearer"
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
