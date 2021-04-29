using Newtonsoft.Json;

namespace TwoFace.Models
{
    public class DetaultModel
    {
        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}
