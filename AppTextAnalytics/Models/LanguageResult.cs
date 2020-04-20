using System;
using System.Collections.Generic;
using System.Text;

namespace AppTextAnalytics.Models
{
    public class LanguageResult
    {
        public Document[] Documents { get; set; }
        public object[] Errors { get; set; }

        public class Document
        {
            public string Id { get; set; }
            public Detectedlanguage[] DetectedLanguages { get; set; }
        }
    }
}
