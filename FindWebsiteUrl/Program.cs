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
        static List<string> values = new List<string>();

        static void Main(string[] args)
        {
            ReadValuesFromFile();
            ReplaceValuesWithPhoneNumbers();
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
                    values.Add(s);
                }
            }
        }

        private static void ReplaceValuesWithPhoneNumbers()
        {
            for (int i = 0; i < values.Count; i++)
            {
                values[i] = FindWebsiteUrl(values[i]);
            }
        }

        private static void WriteWebsiteUrlsToFile()
        {
            using (StreamWriter sw = File.CreateText(writablePath))
            {
                foreach (string v in values)
                {
                    sw.WriteLine(v);
                    Console.WriteLine($"Writing {v}");
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
