using System.Collections.Generic;
using TwoFace.Models;

namespace TwoFace.Responses
{
    public class PersonResponse
    {
        #region Properties
        public List<Person> Result { get; set; }
        public int Status { get; set; }
        #endregion
    }
}
