using AppTextAnalytics.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AppTextAnalytics.Services
{
    public class TextAnalyticsService
    {
        //private string Version = "v2.0";
        private string Version = "v3.0-preview.1";
        /// <summary>
        /// Doc:
        /// https://westus.dev.cognitive.microsoft.com/docs/services/TextAnalytics.V2.0/operations/56f30ceeeda5650db055a3c7
        /// </summary>
        private string _detectLanguageUrl = "https://appbatztextanalytics.cognitiveservices.azure.com/text/analytics/v3.0-preview.1/languages";

        /// <summary>
        /// Doc:
        /// https://westus.dev.cognitive.microsoft.com/docs/services/TextAnalytics.V2.0/operations/56f30ceeeda5650db055a3c6
        /// </summary>
        private string _detectKeyPhrasesUrl = "https://appbatztextanalytics.cognitiveservices.azure.com/text/analytics/v3.0-preview.1/keyPhrases";

        /// <summary>
        /// Doc: 
        /// https://westus.dev.cognitive.microsoft.com/docs/services/TextAnalytics.V2.0/operations/56f30ceeeda5650db055a3c9/
        /// </summary>
        private string _detectSentimentUrl = "https://appbatztextanalytics.cognitiveservices.azure.com/text/analytics/v3.0-preview.1/sentiment";

        /// <summary>
        /// https://westus.dev.cognitive.microsoft.com/docs/services/TextAnalytics-v3-0-Preview-1/operations/EntitiesRecognitionGeneral
        /// </summary>
        private string _detectEntitiesUrl = "https://appbatztextanalytics.cognitiveservices.azure.com/text/analytics/v3.0-preview.1/entities/recognition/general";

        private readonly string _key;

        public TextAnalyticsService(string key)
        {
            _key = key;
        }

        /// <summary>
        /// Detects the language of a given text.
        /// Documentation : 
        /// https://westus.dev.cognitive.microsoft.com/docs/services/TextAnalytics.V2.0/operations/56f30ceeeda5650db055a3c7
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task<LanguageResult> DetectLanguageAsync(string text)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

            var stringContent = new StringContent(@"{""documents"":[{""id"": ""1"",""text"": """ + text + @"""}]}");

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = await httpClient.PostAsync(_detectLanguageUrl, stringContent);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    var detectLanguageResult = JsonConvert.DeserializeObject<LanguageResult>(json);

                    return detectLanguageResult;
                }

                throw new Exception(json);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<KeyPhrasesResult> DetectKeyPhrasesFromTextAsync(string text)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

            var stringContent = new StringContent(@"{""documents"": [{""language"": ""en"",""id"": ""1"",""text"" : """ + text + @"""}]}");

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = await httpClient.PostAsync(_detectKeyPhrasesUrl, stringContent);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    var keyPhrasesResult = JsonConvert.DeserializeObject<KeyPhrasesResult>(json);

                    return keyPhrasesResult;
                }

                throw new Exception(json);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<SentimentResult> DetectSentimentFromTextAsync(string text)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

            var stringContent = new StringContent(@"{""documents"": [{""language"": ""en"",""id"": ""1"",""text"" : """ + text + @"""}]}");

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = await httpClient.PostAsync(_detectSentimentUrl, stringContent);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    var sentimentResult = JsonConvert.DeserializeObject<SentimentResult>(json);

                    return sentimentResult;
                }

                throw new Exception(json);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public async Task<EntitiesResult> DetectEntityFromTextAsync(string text)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

            var stringContent = new StringContent(@"{""documents"": [{""language"": ""en"",""id"": ""1"",""text"" : """ + text + @"""}]}");

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = await httpClient.PostAsync(_detectEntitiesUrl, stringContent);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    var sentimentResult = JsonConvert.DeserializeObject<EntitiesResult>(json);

                    return sentimentResult;
                }

                throw new Exception(json);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
