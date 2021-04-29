using System.Dynamic;

namespace TwoFace.Models
{
    public class Document
    {
        public string DocumentId { get; set; }
        public string DocumentType { get; set; }
        public ExpandoObject Metadata { get; set; }
    }
}
