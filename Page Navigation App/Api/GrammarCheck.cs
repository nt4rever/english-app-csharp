using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Page_Navigation_App.Api
{
    public class GrammarCheck
    {
        public async Task<GrammarCheckResponse> SpellCheck(string text)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://dnaber-languagetool.p.rapidapi.com/v2/check"),
                Headers =
            {
                { "X-RapidAPI-Key", "c62352020emsh4cabfd3f3f1a08fp1ea9e5jsnc37a16335185" },
                { "X-RapidAPI-Host", "dnaber-languagetool.p.rapidapi.com" },
            },
                        Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "language", "en-US" },
                { "text", text },
            }),
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var myDeserializedClass = JsonConvert.DeserializeObject<GrammarCheckResponse>(body);
                return myDeserializedClass;
            }
        }
    }

    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Context
    {
        public string text { get; set; }
        public int offset { get; set; }
        public int length { get; set; }
    }

    public class DetectedLanguage
    {
        public string name { get; set; }
        public string code { get; set; }
        public double confidence { get; set; }
        public string source { get; set; }
    }

    public class Language
    {
        public string name { get; set; }
        public string code { get; set; }
        public DetectedLanguage detectedLanguage { get; set; }
    }

    public class Match
    {
        public string message { get; set; }
        public string shortMessage { get; set; }
        public List<Replacement> replacements { get; set; }
        public int offset { get; set; }
        public int length { get; set; }
        public Context context { get; set; }
        public string sentence { get; set; }
        public Type type { get; set; }
        public Rule rule { get; set; }
        public bool ignoreForIncompleteSentence { get; set; }
        public int contextForSureMatch { get; set; }
    }

    public class Replacement
    {
        public string value { get; set; }
    }

    public class GrammarCheckResponse
    {
        public Software software { get; set; }
        public Warnings warnings { get; set; }
        public Language language { get; set; }
        public List<Match> matches { get; set; }
        public List<List<int>> sentenceRanges { get; set; }
    }

    public class Rule
    {
        public string id { get; set; }
        public string description { get; set; }
        public string issueType { get; set; }
        public List<Url> urls { get; set; }
        public Category category { get; set; }
        public bool isPremium { get; set; }
    }

    public class Software
    {
        public string name { get; set; }
        public string version { get; set; }
        public string buildDate { get; set; }
        public int apiVersion { get; set; }
        public bool premium { get; set; }
        public string premiumHint { get; set; }
        public string status { get; set; }
    }

    public class Type
    {
        public string typeName { get; set; }
    }

    public class Url
    {
        public string value { get; set; }
    }

    public class Warnings
    {
        public bool incompleteResults { get; set; }
    }
}
