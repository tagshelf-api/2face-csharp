using TwoFace.Models;

namespace TwoFace.Responses
{
    public class CreatePersonResponse
    {
        #region Properties
        public Person Result { get; set; }
        public int Status { get; set; }
        #endregion
    }
}
