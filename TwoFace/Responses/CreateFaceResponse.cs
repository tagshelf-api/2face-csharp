using TwoFace.Models;

namespace TwoFace.Responses
{
    public class CreateFaceResponse
    {
        public override string ToString()
        {
            return $"Result: {Result.Message}";
        }
        public DetaultModel Result { get; set; }
    }
}
