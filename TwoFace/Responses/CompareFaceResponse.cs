using TwoFace.Models;

namespace TwoFace.Responses
{
    public class CompareFaceResponse
    {
        public override string ToString()
        {
            return $"Result: {Result.Score}";
        }
        public DefaultThreshold Result { get; set; }
        public int Status { get; set; }
    }
}
