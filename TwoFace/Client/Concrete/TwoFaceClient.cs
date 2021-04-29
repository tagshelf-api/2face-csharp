using RestSharp;
using RestSharp.Deserializers;
using System.Dynamic;
using System.IO;
using TwoFace.Cache.Abstract;
using TwoFace.Client.Abstract;
using TwoFace.Models;
using TwoFace.Responses;
using TwoFace.Tooling.Abstract;

namespace TwoFace.Client.Concrete
{
    public class TwoFaceClient : BaseClient
    {        
        public TwoFaceClient(ICacheService cache,
                         IDeserializer serializer,
                         IErrorLogger errorLogger)
              : base(cache, serializer, errorLogger, "https://2face.tagshelf.io/api")
        { }

        /// <summary>
        /// 2FACE Service Health
        /// </summary>
        /// <returns>
        /// Service name and service version
        /// </returns>
        public HealthResponse Health()
        {
            RestRequest request = new RestRequest("health", Method.GET);
            return Get<HealthResponse>(request);
        }

        /// <summary>
        /// Returns the representation of an entity within 2FACE
        /// </summary>
        /// <param name="documentId">
        /// Unique sequence of number of identification document tied to a person
        /// </param>
        /// <returns>
        /// A given person within the system
        /// </returns>
        public PersonResponse GetPerson(string documentId)
        {
            RestRequest request = new RestRequest($"people?documentId={documentId}", Method.GET);
            return Get<PersonResponse>(request);
        }

        /// <summary>
        /// Creates a new person within 2FACE
        /// </summary>
        /// <param name="documentId">
        /// Unique sequence of number of identification document tied to a person
        /// </param>
        /// <param name="documentType">
        /// Unique identifier for the document type of the identification document tied to a person
        /// i.e. government issued id or passport
        /// </param>
        /// <param name="metadata">
        /// Context specific information tied to a given person
        /// i.e. use this for extra information to be stored
        /// </param>
        /// <returns>
        /// Create person response
        /// </returns>
        public CreatePersonResponse CreatePerson(string documentId, string documentType, ExpandoObject metadata)
        {
            var document = new Document
            {
                DocumentId = documentId,
                DocumentType = documentType,
                Metadata = metadata
            };

            RestRequest request = new RestRequest($"people");
            request.AddJsonBody(document);
            return Post<CreatePersonResponse>(request);
        }

        /// <summary>
        /// Creates a new person within 2FACE
        /// </summary>
        /// <param name="document">
        /// Model which represents a document
        /// DocumentId, DocumentType and Metadata should be set.
        /// </param>
        /// <returns>
        /// Create person response
        /// </returns>
        public CreatePersonResponse CreatePerson(Document document)
        {
            RestRequest request = new RestRequest($"people");
            request.AddJsonBody(document);
            return Post<CreatePersonResponse>(request);
        }

        /// <summary>
        /// Adds a face to a given person
        /// </summary>
        /// <param name="personId">
        /// A person's unique identifier within 2FACE context
        /// </param>
        /// <param name="filePath">
        /// Local file system path to image of the face
        /// </param>
        /// <param name="fileName">
        /// File name for the given image being sent (optional)
        /// </param>
        /// <returns>
        /// Add face response
        /// </returns>
        public CreateFaceResponse AddFace(string personId, string filePath, string fileName = "")
        {
            if (string.IsNullOrEmpty(fileName)) fileName = Path.GetFileName(filePath);
            byte[] file = File.ReadAllBytes(filePath);
            return AddFace(personId, file, fileName);
        }

        /// <summary>
        /// Adds a face to a given person
        /// </summary>
        /// <param name="personId">
        /// A person's unique identifier within 2FACE context
        /// </param>
        /// <param name="fileContent">
        /// Byte array containing the user's image data
        /// </param>
        /// <param name="fileName">
        /// File name for the given image being sent
        /// </param>
        /// <returns>
        /// Add face response
        /// </returns>
        public CreateFaceResponse AddFace(string personId, byte[] fileContent, string fileName)
        {
            RestRequest request = new RestRequest($"people/{personId}/faces");
            request.AddFile("face", fileContent, fileName);
            return Post<CreateFaceResponse>(request);
        }

        /// <summary>
        /// Compares 2 FACEs
        /// </summary>
        /// <param name="filePathA">
        /// Path to first file
        /// </param>
        /// <param name="filePathB">
        /// Path to second file
        /// </param>
        /// <param name="fileNameFaceA">
        /// First file name
        /// </param>
        /// <param name="fileNameFaceB">
        /// Second file name
        /// </param>
        /// <returns>
        /// Score of 2 face comparison
        /// </returns>
        public CompareFaceResponse CompareFaces(string filePathA, string filePathB, string fileNameFaceA = "", string fileNameFaceB = "")
        {
            if (string.IsNullOrEmpty(fileNameFaceA)) Path.GetFileName(filePathA);
            if (string.IsNullOrEmpty(fileNameFaceB)) Path.GetFileName(filePathB);

            RestRequest request = new RestRequest($"faces/compare");                        
            request.AddFile("faceA", filePathA, fileNameFaceA);
            request.AddFile("faceB", filePathB, fileNameFaceB);
            return Post<CompareFaceResponse>(request);
        }

        /// <summary>
        /// Compares 2 FACEs
        /// </summary>
        /// <param name="fileA">
        /// Byte array containing the first image data
        /// </param>
        /// <param name="fileNameFaceA">
        /// First file name
        /// </param>
        /// <param name="fileB">
        /// Byte array containing the second image data
        /// </param>
        /// <param name="fileNameFaceB">
        /// Second file name
        /// </param>
        /// <returns>
        /// Score of 2 face comparison
        /// </returns>
        public CompareFaceResponse CompareFaces(byte[] fileA, string fileNameFaceA, byte[] fileB, string fileNameFaceB)
        {
            RestRequest request = new RestRequest($"faces/compare");
            request.AddFile("faceA", fileA, fileNameFaceA);
            request.AddFile("faceB", fileB, fileNameFaceB);
            return Post<CompareFaceResponse>(request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public CompareFaceToBaselineResponse CompareFaceToBaseline(string documentId, string filePath)
        {            
            RestRequest request = new RestRequest($"faces/compareByRef");
            request.AddParameter("ref", $"{{\"document_id\": \"{documentId}\"}}");
            request.AddFile("face", filePath);
            return Post<CompareFaceToBaselineResponse>(request);
        }
    }
}
