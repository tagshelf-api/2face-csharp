using TwoFace.Models;

namespace TwoFace.Responses
{
    public class CompareFaceToBaselineResponse
    {
        public Match Result { get; set; }
        public int Status { get; set; }
    }
}
