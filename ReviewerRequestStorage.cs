using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ConferenceApp
{
    public static class ReviewerRequestStorage
    {
        private static readonly string filePath =
            Path.Combine(Application.StartupPath, "reviewer_requests.json");

        public static List<ReviewerRequest> Load()
        {
            if (!File.Exists(filePath))
            {
                return new List<ReviewerRequest>();
            }

            string json = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<ReviewerRequest>();
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            try
            {
                return serializer.Deserialize<List<ReviewerRequest>>(json)
                       ?? new List<ReviewerRequest>();
            }
            catch
            {
                return new List<ReviewerRequest>();
            }
        }

        public static void Save(List<ReviewerRequest> requests)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(requests);
            File.WriteAllText(filePath, json);
        }

        public static bool EmailExists(string email)
        {
            return Load().Any(r =>
                string.Equals(r.Email, email, StringComparison.OrdinalIgnoreCase));
        }

        public static void Add(ReviewerRequest request)
        {
            List<ReviewerRequest> requests = Load();
            requests.Add(request);
            Save(requests);
        }
    }
}