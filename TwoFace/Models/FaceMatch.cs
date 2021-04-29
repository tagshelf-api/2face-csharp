using Newtonsoft.Json;
using System.Dynamic;

namespace TwoFace.Models
{
    public class FaceMatch
    {        
        [JsonProperty("avg_score")]
        public double AverageScore { get; set; }
        public string Id { get; set; }
        public string DocumentId { get; set; }
        public string DocumentType { get; set; }
        public ExpandoObject Metadata { get; set; }
    }
}
