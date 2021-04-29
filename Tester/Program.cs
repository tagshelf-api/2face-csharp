using System;
using System.Collections.Generic;
using System.IO;
using TwoFace.Cache.Concrete;
using TwoFace.Client.Concrete;
using TwoFace.Serialization.Concrete;
using TwoFace.Tooling.Concrete;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            string cedula = "6383736743891101";
            var dictionary = new Dictionary<string, object>();
            var logger = new ErrorLogger();
            var twoFace = new TwoFaceClient(new InMemoryCache(), new JsonSerializer(), logger);

            #region Health
            /*
             * Allows for service health checks
             */
            var health = twoFace.Health();
            Console.WriteLine(health);
            #endregion

            #region Create a Person
            /*
             * Create a person
             */
            //dynamic meta = new ExpandoObject();
            //meta.Age = 36;
            //meta.Country = "Dominican Republic";
            //var newPerson = twoFace.CreatePerson(cedula, "CEDULA", meta);
            //// Parse custom metadata field            
            //dictionary = new Dictionary<string, object>(newPerson.Result.Metadata);
            //Console.WriteLine("=================================================");
            //Console.WriteLine("CREATED PERSON METADATA & RESULTS");
            //Console.WriteLine("=================================================");
            //Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(newPerson, Newtonsoft.Json.Formatting.Indented));
            //Console.WriteLine("\n");
            //Console.WriteLine("METADATA MANIPULATION EXAMPLE");
            //Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(dictionary, Newtonsoft.Json.Formatting.Indented));
            //Console.WriteLine("\n");
            #endregion

            #region Add Face
            /*
             * Add a face to a given person
             */
            //string fileName = "Document.png";
            //string path = Path.Combine(
            //    new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName, @"Images\", fileName);
            //var addFaceResult = twoFace.AddFace(newPerson.Result.Id, path);
            //Console.WriteLine("=================================================");
            //Console.WriteLine("ADDIN FACE BASELINE");
            //Console.WriteLine("=================================================");
            //Console.WriteLine(addFaceResult);
            //Console.WriteLine("\n");
            #endregion

            #region Match faces
            /*
             * Match 2 faces
             */
            //string fileNameA = "02.jpg";
            //string fileNameB = "Document.png";
            //string pathA = Path.Combine(
            //    new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName, @"Images\", fileNameA);
            //string pathB = Path.Combine(
            //    new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName, @"Images\", fileNameB);

            //Console.WriteLine("=================================================");
            //Console.WriteLine("COMPARES FACE BASELINE WITH ANOTHER FACE");
            //Console.WriteLine("=================================================");
            //var comparisonResult = twoFace.CompareFaces(pathA, pathB);
            //Console.WriteLine(comparisonResult);
            //Console.WriteLine("\n");
            #endregion

            #region Get Person
            // Retrieve existing person
            var existingPerson = twoFace.GetPerson(cedula);
            // Parse custom metadata field            
            dictionary = new Dictionary<string, object>(existingPerson.Result[0].Metadata);
            Console.WriteLine("=================================================");
            Console.WriteLine("RETRIEVED PERSON METADATA & RESULTS");
            Console.WriteLine("=================================================");
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(existingPerson, Newtonsoft.Json.Formatting.Indented));
            Console.WriteLine("\n");
            Console.WriteLine("METADATA MANIPULATION EXAMPLE");
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(dictionary, Newtonsoft.Json.Formatting.Indented));
            Console.WriteLine("\n");
            #endregion

            #region Matches against baseline
            Console.WriteLine("=================================================");
            Console.WriteLine("RETRIEVED MATCHES AGAINST BASELINE");
            Console.WriteLine("=================================================");
            string baseLine = "03.jpg";
            string baseLinePath = Path.Combine(
                new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName, @"Images\", baseLine);
            var res = twoFace.CompareFaceToBaseline("00117135939", baseLinePath);
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(res, Newtonsoft.Json.Formatting.Indented));
            Console.WriteLine("\n");
            #endregion

            Console.ReadKey();
        }
    }
}
