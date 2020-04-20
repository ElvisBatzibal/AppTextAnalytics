using System;
using System.Collections.Generic;
using System.Text;

namespace AppTextAnalytics.Models
{
    public class EntitiesResult
    {
        public List<Document> Documents { get; set; }
        public List<object> Errors { get; set; }
        public string Modelversion { get; set; }
        /// <summary>
        /// Return Full Text Documents String 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string FullText = string.Empty;
            foreach (var item in Documents)
            {
                FullText += item.ToString();
            }
            return FullText;
        }

        public class Document
        {
            public string Id { get; set; }
            public List<Entity> Entities { get; set; }
            /// <summary>
            /// Return full text Entity Doc 
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                string TextFull = string.Empty;
                foreach (var item in Entities)
                {
                    TextFull += item.Text + " [" + item.Type + "] ";
                }
                return TextFull;
            }
        }
    }
    public class Entity
    {
        public string Text { get; set; }
        public string Type { get; set; }
        public string Subtype { get; set; }
        public int Offset { get; set; }
        public int Length { get; set; }
        public double Score { get; set; }
    }
}
