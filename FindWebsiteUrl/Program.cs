using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace FindWebsiteUrl
{
    class Program
    {
        //EDIT THESE FILES PATHS FOR YOUR USAGE
        static readonly string readablePath = @"C:\Users\User\Desktop\someFile.csv";
        static string writablePath = @"C:\Users\User\Desktop\websiteList.csv";


        static readonly Regex websiteUrlRg = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        //temp variable(s)
        static List<record> values = new List<record>();

        public class record
        {
            public string ProductId { get; set; }
            public string Url { get; set; }
            public string WebsiteUrl { get; set; }
        }

        static void Main(string[] args)
        {
            ReadValuesFromFile();
            WriteWebsiteUrlsToFile();
            Console.WriteLine("Complete");
        }

        private static void ReadValuesFromFile()
        {
            using (StreamReader sr = File.OpenText(readablePath))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    values.Add(new record {
                        ProductId = s.Split(',')[0],
                        Url = s.Substring(s.IndexOf(',')),
                        WebsiteUrl = FindWebsiteUrl(s)
                    });
                }
            }
        }

        private static void WriteWebsiteUrlsToFile()
        {
            using (StreamWriter sw = File.CreateText(writablePath))
            {
                foreach (record v in values)
                {
                    sw.WriteLine($"{v.ProductId},{v.Url},{v.WebsiteUrl}");
                    Console.WriteLine($"Writing {v.ProductId}");
                }
            }
        }

        private static string FindWebsiteUrl(string s)
        {
            if (String.IsNullOrWhiteSpace(s)) return "";

            Match result = websiteUrlRg.Match(s);

            return result.Value;
        }

        //Not Required  - would be added as a helper in FindWebsiteUrl()
        private static string RemoveWhiteSpace(string s)
        {
            return Regex.Replace(s, @"\s+", "");
        }
    }
}
