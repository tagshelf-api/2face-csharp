using System.Collections.Generic;
using System.Dynamic;

namespace TwoFace.Models
{
    public class Person
    {
        #region Properties
        public string Id { get; set; }
        public string DocumentId { get; set; }
        public string DocumentType { get; set; }
        public List<List<double>> FaceEmbeddings { get; set; }
        public ExpandoObject Metadata { get; set; }
        #endregion
    }
}
