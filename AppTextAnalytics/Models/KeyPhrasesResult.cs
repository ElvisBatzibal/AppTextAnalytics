using System;
using System.Collections.Generic;
using System.Text;

namespace AppTextAnalytics.Models
{
    /// <summary>
    /// Detectar las palabras claves del texto
    /// </summary>
    public class KeyPhrasesResult
    {
        public Document[] Documents { get; set; }
        public object[] Errors { get; set; }
        /// <summary>
        /// Return all text Key Phrases Result a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string FullTextKey = string.Empty;
            foreach (var item in Documents)
            {
                FullTextKey += item.ToString();
            }
            return FullTextKey;
        }

        public class Document
        {
            public List<string> KeyPhrases { get; set; }
            public string Id { get; set; }
            /// <summary>
            /// Return Full Text to Array KeyPhrases
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                string FullText = string.Empty;
                if (KeyPhrases.Count > 0)
                {
                    FullText = string.Join(",", KeyPhrases.ToArray());
                }
                return FullText;
            }
        }
    }
}
